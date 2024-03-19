using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.CatalogRoomType;
using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Room
{
    public record RoomsByOperationIdQuery(int OperationId) : IRequest<Result<List<RoomsByIdOperationResponse>>>;

    public class RoomsByOperationIdHandler : IRequestHandler<RoomsByOperationIdQuery, Result<List<RoomsByIdOperationResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        private readonly ISender _sender;
        public RoomsByOperationIdHandler(IRepositorySharedForReadManager repositoryClient,
            ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<List<RoomsByIdOperationResponse>>> Handle(RoomsByOperationIdQuery request, CancellationToken cancellationToken)
        {
            var operation = await _repositoryClient.Operations.SingleOrDefaultAsync(new OperationByIdIncludeRoomsSpecs(request.OperationId, true), cancellationToken);

            if (operation is null)
            {
                return Result.Failure<List<RoomsByIdOperationResponse>>("Operación no encontrada");
            }
            if (!operation.Rooms.Any())
            {
                return Result.Failure<List<RoomsByIdOperationResponse>>("Habitaciones no encontradas");
            }

            var catalog = await _sender.Send(new CatalogRoomTypeQuery(), cancellationToken);


            return Result.Success(operation.Rooms.Select(x => new RoomsByIdOperationResponse(
                Id: x.Id,
                Description: x.Description,
                UrlLogo: x.UrlLogo,
                Title: x.Title,
                TypeRoom: catalog.Value.First(y => y.Id == x.RoomTypeId).Description,
                null,
                Observation: x.Observation,
                Guests: x.Guests
                )).ToList());
        }
    }
}