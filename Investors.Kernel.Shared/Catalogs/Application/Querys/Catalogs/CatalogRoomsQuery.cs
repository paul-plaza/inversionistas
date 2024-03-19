using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs
{
    public record class CatalogRoomsQuery : IRequest<Result<IReadOnlyCollection<CatalogResponse>>>;

    public class CatalogRoomsHandler : IRequestHandler<CatalogRoomsQuery, Result<IReadOnlyCollection<CatalogResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogRoomsHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<IReadOnlyCollection<CatalogResponse>>> Handle(CatalogRoomsQuery request, CancellationToken cancellationToken)
        {
            var catalogs = await _repositoryClient.Catalogs.ListAsync(new RoomsSpecs(), cancellationToken);

            if (!catalogs.Any())
            {
                return Result.Failure<IReadOnlyCollection<CatalogResponse>>("Catalogs not found");
            }
            var rooms = catalogs.Select(catalog => new CatalogResponse(
                catalog.Id,
                catalog.Description,
                catalog.CatalogDetails.Select(catalogDetail => new CatalogDetailResponse(
                    catalogDetail.Id,
                    catalogDetail.Description,
                    catalogDetail.Value
                    )).ToList()
                )).ToList();
            return Result.Success<IReadOnlyCollection<CatalogResponse>>(rooms);
        }
    }
}