using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs
{
    public record class CatalogUnauthorizedRecordsForPointsCatalogQuery : IRequest<Result<IReadOnlyCollection<CatalogResponse>>>;

    public class CatalogRestaurantsHandler : IRequestHandler<CatalogUnauthorizedRecordsForPointsCatalogQuery, Result<IReadOnlyCollection<CatalogResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogRestaurantsHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<IReadOnlyCollection<CatalogResponse>>> Handle(CatalogUnauthorizedRecordsForPointsCatalogQuery request, CancellationToken cancellationToken)
        {
            var events = await _repositoryClient.Catalogs.ListAsync(new UnauthorizedRecordsForPointsSpecs(), cancellationToken);

            if (!events.Any())
            {
                return Result.Failure<IReadOnlyCollection<CatalogResponse>>("Catalogs not found");
            }
            var operationList = events.Select(catalog => new CatalogResponse(
                catalog.Id,
                catalog.Description,
                catalog.CatalogDetails.Select(catalogDetail => new CatalogDetailResponse(
                    catalogDetail.Id,
                    catalogDetail.Description,
                    catalogDetail.Value
                    )).ToList()
                )).ToList();
            return Result.Success<IReadOnlyCollection<CatalogResponse>>(operationList);
        }
    }
}