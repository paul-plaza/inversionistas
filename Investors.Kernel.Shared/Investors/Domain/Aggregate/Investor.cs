using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Domain.Entities;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Investors.Domain.Aggregate
{
    public class Investor : BaseEntity<Guid>
    {

        public const int IdentificationMaxLength = 20;

        public const int FullNamesMaxLength = 250;

        /// <summary>
        /// Nombre completo del inversionista
        /// </summary>
        public string FullNames { get; private set; }


        /// <summary>
        /// Identificacion del inversionista, ci, ruc, pasaporte
        /// </summary>
        public string Identification { get; private set; }


        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<InvestorOperation> _investorOperations = new();

        /// <summary>
        /// Detalle de factura 
        /// </summary>
        public virtual IReadOnlyList<InvestorOperation> InvestorOperations => _investorOperations;


        protected Investor()
        {

        }

        private Investor(
            Guid id,
            string fullNames,
            string identification,
            IEnumerable<InvestorOperation> investorOperations,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            Id = id;
            FullNames = fullNames;
            Identification = identification;
            _investorOperations.AddRange(investorOperations);
        }

        public static Result<Investor> Create(
            string fullNames,
            string identification,
            IEnumerable<Tuple<int, int>> investorOperations,
            Guid createBy)
        {
            Guard.Against.NullOrWhiteSpace(fullNames);
            Guard.Against.NullOrWhiteSpace(identification);
            var id = Guid.NewGuid();
            var operations = investorOperations.Select(x =>
                InvestorOperation.Create(x.Item1, x.Item2, id, createBy)).ToList();

            fullNames = fullNames.Trim();
            identification = identification.Trim();

            if (identification.Length > IdentificationMaxLength)
            {
                Errors.Add(ValidationConstants.ValidateMaxLength(nameof(identification), IdentificationMaxLength));
                Errors.Add("Valor ingresado: " + identification);
            }

            if (fullNames.Length > FullNamesMaxLength)
            {
                Errors.Add(ValidationConstants.ValidateMaxLength(nameof(fullNames), FullNamesMaxLength));
                Errors.Add("Valor ingresado: " + fullNames);
            }

            if (Errors.Any())
                return Result.Failure<Investor>(GetErrors());


            return Result.Success(new Investor(
                Guid.NewGuid(),
                fullNames.Trim(),
                identification.Trim(),
                operations.Select(x => x.Value),
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result UpdateInvestor(
            string fullNames,
            List<Tuple<int, int>> operations,
            Guid userInSession)
        {
            Guard.Against.NullOrWhiteSpace(fullNames);

            //recorro la lista de operaciones y activo al existente y agrego los nuevos
            operations.ForEach(x =>
            {
                var operation = _investorOperations.FirstOrDefault(y => y.OperationId == x.Item1);
                if (operation is null)
                {
                    var investorOperation = InvestorOperation.Create(x.Item1, x.Item2, Id, userInSession);
                    if (investorOperation.IsSuccess)
                    {
                        _investorOperations.Add(investorOperation.Value);
                    }
                }
                else
                {
                    operation.UpdateOperation(x.Item2, userInSession);
                }
            });
            Update(userInSession);
            return Result.Success();
        }

        public void DeleteInvestor(Guid userInSession)
        {
            _investorOperations.ForEach(x => x.DeleteOperation(userInSession));
            Delete(userInSession);
        }

    }
}