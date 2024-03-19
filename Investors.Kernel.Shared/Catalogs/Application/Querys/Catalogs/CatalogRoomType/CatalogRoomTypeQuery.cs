using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.CatalogRoomType
{
    public record CatalogRoomTypeQuery : IRequest<Result<IReadOnlyCollection<CatalogRoomTypeResponse>>>;

    public class CatalogRoomTypeHandler : IRequestHandler<CatalogRoomTypeQuery, Result<IReadOnlyCollection<CatalogRoomTypeResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogRoomTypeHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<IReadOnlyCollection<CatalogRoomTypeResponse>>> Handle(CatalogRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var catalogs = await _repositoryClient.Catalogs.ListAsync(new RoomsTypeSpecs(), cancellationToken);

            if (!catalogs.Any())
            {
                return Result.Failure<IReadOnlyCollection<CatalogRoomTypeResponse>>("Habitaciones no configuradas.");
            }
            var rooms = catalogs
                .SelectMany(x => x.CatalogDetails)
                .Select(catalog => new CatalogRoomTypeResponse(
                    catalog.Id,
                    catalog.Description
                    )).ToList();
            return Result.Success<IReadOnlyCollection<CatalogRoomTypeResponse>>(rooms);
        }
    }
}