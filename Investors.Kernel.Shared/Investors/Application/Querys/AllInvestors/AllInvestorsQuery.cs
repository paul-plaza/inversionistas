using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Investors.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Investors.Application.Querys.AllInvestors
{

    public record AllInvestorsQuery : IRequest<Result<IEnumerable<AllInvestorsResponse>>>;

    public class AllInvestorsHandler : IRequestHandler<AllInvestorsQuery, Result<IEnumerable<AllInvestorsResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryShared;

        public AllInvestorsHandler(IRepositorySharedForReadManager repositoryShared)
        {
            _repositoryShared = repositoryShared;
        }

        public async Task<Result<IEnumerable<AllInvestorsResponse>>> Handle(AllInvestorsQuery request, CancellationToken cancellationToken)
        {
            var investors = await _repositoryShared.Investors.ListAsync(new AllInvestorsActiveSpecs(), cancellationToken);

            return Result.Success(investors.Select(x => new AllInvestorsResponse
            {
                Identification = x.Identification,
                Names = x.FullNames,
                Operations = x.InvestorOperations.Select(operation => new OperationsResponse
                {
                    Id = operation.OperationId,
                    Description = operation.Operation.Description,
                    UlrLogo = operation.Operation.UrlLogo,
                    TotalActions = operation.TotalActions
                }).ToList()
            }));
        }
    }

}