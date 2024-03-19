using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationAndRoomById
{
    public record OperationAndRoomByIdQuery(int OperationId, int RoomId) : IRequest<Result<OperationByIdIncludeRoomResponse>>;

    public class OperationAndRoomById : IRequestHandler<OperationAndRoomByIdQuery, Result<OperationByIdIncludeRoomResponse>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public OperationAndRoomById(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<OperationByIdIncludeRoomResponse>> Handle(OperationAndRoomByIdQuery request, CancellationToken cancellationToken)
        {

            var operation = await _repositoryClient.Operations
                .SingleOrDefaultAsync(new OperationIncludeRoomByIdSpecs(request.OperationId, request.RoomId), cancellationToken);

            if (operation is null)
            {
                return Result.Failure<OperationByIdIncludeRoomResponse>("Operacion no encontradas");
            }

            return Result.Success(new OperationByIdIncludeRoomResponse(
                Description: operation.Description,
                UrlLogo: operation.UrlLogo,
                OperationId: operation.Id,
                Email: operation.Email,
                Room: new RoomByIdOperationResponse(
                    Description: operation.Rooms.First().Description,
                    UrlLogo: operation.Rooms.First().UrlLogo,
                    Title: operation.Rooms.First().Title
                    )
                ));
        }
    }
}