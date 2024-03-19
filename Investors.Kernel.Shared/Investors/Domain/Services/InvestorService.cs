using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Application.Commands.InvestorManagement;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Investors.Domain.Repositories;
using Investors.Kernel.Shared.Investors.Specifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Investors.Domain.Services
{
    public class InvestorService
    {
        private readonly IInvestorWriteRepository _investorRepository;
        private readonly ISender _sender;
        public InvestorService(IInvestorWriteRepository investorRepository, ISender sender)
        {
            _investorRepository = investorRepository;
            _sender = sender;
        }

        public async Task<Result<ResponseDefault>> RegisterInvestor(
            List<InvestorManagementRequest> investorsManagement,
            Guid createBy,
            CancellationToken cancellationToken)
        {
            var operationsInDb = await _sender.Send(new GetOperationsQuery(), cancellationToken);

            if (operationsInDb.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Operaciones no encontradas");
            }

            //hago limpieza de datos a la columna identificacion
            investorsManagement.ForEach(x =>
            {
                if (x.Identification.Contains(":"))
                {
                    //remuevo el caracter : y tomo la posicion 1
                    x.Identification = x.Identification.Split(":")[1];
                }
                //remuevo caracteres especiales
                x.Identification = Regex.Replace(x.Identification, @"[^0-9a-zA-Z\s]", "");
            });

            //verifico si todos los id de operacion existen
            var operations = investorsManagement.Select(x => x.OperationId).Distinct().ToList();
            //verifico si todos los id de operacion existen de lo contrario retorno error
            var operationsNotExists = operations.All(x => operationsInDb.Value.Select(upDb => upDb.Id).Contains(x));

            if (!operationsNotExists)
            {
                return Result.Failure<ResponseDefault>("Por favor verifique las operaciones agregadas existan!");
            }

            //obtengo todos los inversionistas con sus operaciones
            var investors = await _investorRepository.ListAsync(new AllInvestorsSpecs(), cancellationToken);


            //cambio a estado elimiado a todos para solo reactivar a los que existen en la nueva lista
            investors.ForEach(x => x.DeleteInvestor(userInSession: createBy));

            var investorGroup = investorsManagement.GroupBy(x => x.Identification).ToList();

            var itemsToUpdate =
                (from im in investorGroup
                    join i in investors on im.Key equals i.Identification
                    select i.UpdateInvestor(
                        fullNames: im.First().FullNames,
                        operations: im.Select(x => new Tuple<int, int>(x.OperationId, x.TotalActions)).ToList(),
                        userInSession: createBy)).ToList();

            //si alguno esta con error retornar
            if (itemsToUpdate.Any(x => x.IsFailure))
            {
                var allErrors = itemsToUpdate.Where(x => x.IsFailure)
                    .Select(x => x.Error)
                    .Aggregate((current, next) => current + "; " + next);

                return Result.Failure<ResponseDefault>(allErrors);
            }


            var itemsToInsert =
                (from im in investorGroup
                    join i in investors on im.Key equals i.Identification into joined
                    from subInvestor in joined.DefaultIfEmpty()
                    where subInvestor == null
                    select Investor.Create(
                        identification: im.First().Identification,
                        fullNames: im.First().FullNames,
                        investorOperations: im.Select(x => new Tuple<int, int>(x.OperationId, x.TotalActions)),
                        createBy: createBy
                        )).ToList();

            if (itemsToInsert.Any(x => x.IsFailure))
            {
                var allErrors = itemsToInsert.Where(x => x.IsFailure)
                    .Select(x => x.Error)
                    .Aggregate((current, next) => current + "; " + next);

                return Result.Failure<ResponseDefault>(allErrors);
            }

            await _investorRepository.AddRangeAsync(itemsToInsert.Select(x => x.Value), cancellationToken);
            await _investorRepository.SaveChangesAsync(cancellationToken);

            await _investorRepository.ExecuteSyncUserInvestor(cancellationToken);

            ResponseDefault result = new ResponseDefault("Inversionistas se han registrado correctamente");
            return Result.Success(result);
        }
    }
}