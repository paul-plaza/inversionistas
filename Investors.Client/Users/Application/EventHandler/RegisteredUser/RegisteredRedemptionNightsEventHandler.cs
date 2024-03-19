using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationAndRoomById;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.RegisteredUser
{
    internal class RegisteredRedemptionNightsEventHandler : INotificationHandler<RegisteredRedemptionNightsEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        public RegisteredRedemptionNightsEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }
        public async Task Handle(RegisteredRedemptionNightsEvent notification, CancellationToken cancellationToken)
        {
            var operationRoom =
                await _sender.Send(new OperationAndRoomByIdQuery(notification.OperationId, notification.RoomId), cancellationToken);

            if (operationRoom.IsFailure)
            {
                return;
            }

            //obtengo url de api de administrador para aceptar o rechazar la orden
            var catalog = await _sender.Send(new CatalogUrlAdministratorApiQuery(), cancellationToken);

            await _sender.Send(new RegisterNotificationCommand(
                notification.Id,
                "Felicitaciones orden creada",
                null,
                NotificationType.Nights,
                "Hemos recibido su solicitud de habitaciones",
                notification.OperationId,
                notification.UserInSession), cancellationToken);

            await _managementNotifications.EditUserTag(notification.Id, new
            {
                first_name = notification.UserName
            }, cancellationToken);

            //envio push
            await _managementNotifications.SentPushToUserId(
                notification.Id,
                new Guid(TemplateNotification.RegisterNightsPush),
                cancellationToken);

            //envio mail al usuario
            await _managementNotifications.SentEmailToUserId(
                notification.Id,
                new Guid(TemplateNotification.RegisterNightsEmailUser),
                "Nueva orden de canje de noches", new
                {
                    user_name = notification.UserName,
                    operation_name = operationRoom.Value.Description,
                    operation_image = operationRoom.Value.UrlLogo,
                    operation_room = operationRoom.Value.Room.Description,
                    operation_title = operationRoom.Value.Room.Title,
                    checkIn = notification.DateStart.ToString("yyyy-MM-dd"),
                    checkOut = notification.DateEnd.ToString("yyyy-MM-dd")
                },
                cancellationToken);

            //envio mail al recepcionista
            await _managementNotifications.SentEmailToAdmin(
                new Guid(TemplateNotification.RegisterNightsEmailAdmin),
                "Nuevo Orden de compras de inversionista", operationRoom.Value.Email, new
                {
                    operation_name = operationRoom.Value.Description,
                    operation_image = operationRoom.Value.UrlLogo,
                    operation_title = operationRoom.Value.Room.Title,
                    operation_room = operationRoom.Value.Room.Description,
                    url_accept = $"{catalog.Value}{notification.Id}/nights/{notification.MovementId}/accept",
                    url_reject = $"{catalog.Value}{notification.Id}/nights/{notification.MovementId}/reject",
                    user_name = notification.UserName,
                    user_identification = notification.Identification,
                    checkIn = notification.DateStart.ToString("yyyy-MM-dd"),
                    checkOut = notification.DateEnd.ToString("yyyy-MM-dd")
                },
                cancellationToken);
        }
    }
}