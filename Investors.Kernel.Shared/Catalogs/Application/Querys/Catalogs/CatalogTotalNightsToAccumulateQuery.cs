using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs
{
    public record class CatalogTotalNightsToAccumulateQuery : IRequest<Result<int>>;

    public class CatalogTotalNightsToAccumulateHandler : IRequestHandler<CatalogTotalNightsToAccumulateQuery, Result<int>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogTotalNightsToAccumulateHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<int>> Handle(CatalogTotalNightsToAccumulateQuery request, CancellationToken cancellationToken)
        {
            var totalNights = await _repositoryClient.Catalogs.SingleOrDefaultAsync(new TotalNightsToAccumulateSpecs(), cancellationToken);

            return Result.Success(totalNights);
        }
    }
}