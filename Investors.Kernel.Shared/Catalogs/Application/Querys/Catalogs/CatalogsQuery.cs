using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs
{
    public record CatalogsQuery : IRequest<Result<IReadOnlyCollection<CatalogResponse>>>;

    public class CatalogsHandler : IRequestHandler<CatalogsQuery, Result<IReadOnlyCollection<CatalogResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogsHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<IReadOnlyCollection<CatalogResponse>>> Handle(CatalogsQuery request, CancellationToken cancellationToken)
        {
            var events = await _repositoryClient.Catalogs.ListAsync(new CatalogsSpecs(), cancellationToken);

            if (!events.Any())
            {
                return Result.Failure<IReadOnlyCollection<CatalogResponse>>("Catalogs not found");
            }
            var catalogs = events.Select(catalog => new CatalogResponse(
                catalog.Id,
                catalog.Description,
                catalog.CatalogDetails.Select(catalogDetail => new CatalogDetailResponse(
                    catalogDetail.Id,
                    catalogDetail.Description,
                    catalogDetail.Value
                    )).ToList()
                )).ToList();
            return Result.Success<IReadOnlyCollection<CatalogResponse>>(catalogs);
        }
    }
}