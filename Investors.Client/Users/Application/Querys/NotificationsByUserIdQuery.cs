using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Specifications;
using MediatR;

namespace Investors.Client.Users.Application.Querys
{

    public record NotificationsByUserIdQuery(Guid UserId) : IRequest<Result<List<NotificationsByUserIdResponse>>>;

    public class NotificationsByUserIdHandler : IRequestHandler<NotificationsByUserIdQuery, Result<List<NotificationsByUserIdResponse>>>
    {
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public NotificationsByUserIdHandler(IRepositoryClientForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<List<NotificationsByUserIdResponse>>> Handle(NotificationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repositoryClient.Users.SingleOrDefaultAsync(new NotificationByUserIdSpecs(request.UserId, true), cancellationToken);

            if (user is null)
            {
                return Result.Failure<List<NotificationsByUserIdResponse>>("Usuario no encontrado");
            }
            if (!user.Notifications.Any())
            {
                return Result.Failure<List<NotificationsByUserIdResponse>>("No se encontró notificaciones de este usuario");
            }
            return Result.Success(user.Notifications);
        }
    }
}