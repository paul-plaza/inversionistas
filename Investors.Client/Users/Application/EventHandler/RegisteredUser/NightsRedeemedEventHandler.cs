using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Kernel.Shared.Operations.Application.Querys.Room;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.RegisteredUser
{
    internal class NightsRedeemedEventHandler : INotificationHandler<NightsRedeemedEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        public NightsRedeemedEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }
        public async Task Handle(NightsRedeemedEvent notification, CancellationToken cancellationToken)
        {
            var operations = await _sender.Send(new GetOperationsQuery(), cancellationToken);
            var operation = operations.Value.First(x => x.Id == notification.Movement.OperationId);
            var rooms = await _sender.Send(new RoomsByOperationIdQuery(notification.Movement.OperationId), cancellationToken);
            var room = rooms.Value.Where(room => room.Id == notification.Movement.RoomId);
            string message = "Orden aprobada, disfruta de tu estadía";
            string templatePush = TemplateNotification.NightsRedeemedPush;
            string templateEmail = TemplateNotification.NightsRedeemedEmail;

            if (string.Equals(notification.State, "Rechazado"))
            {
                message = "Su orden de hospedaje ha sido rechazada";
                templatePush = TemplateNotification.NightsRejectedPush;
                templateEmail = TemplateNotification.NightsRejectedEmail;
            }

            if (string.Equals(notification.State, "Cancelado"))
            {
                message = "Su orden de hospedaje se canceló con éxito";
                templatePush = TemplateNotification.NightsCancelledPush;
                templateEmail = TemplateNotification.NightsCancelledEmail;
            }

            await _sender.Send(new RegisterNotificationCommand(
                notification.Id,
                "Su orden de hospedaje ha sido " + notification.State,
                null,
                NotificationType.Nights,
                message,
                operation.Id,
                notification.UserInSession), cancellationToken);

            await _managementNotifications.EditUserTag(notification.Id, new
            {
                first_name = notification.UserName
            }, cancellationToken);

            //envio push
            await _managementNotifications.SentPushToUserId(
                notification.Id,
                new Guid(templatePush),
                cancellationToken);

            //envio mail al usuario
            await _managementNotifications.SentEmailToUserId(
                notification.Id,
                new Guid(templateEmail),
                "Su orden de hospedaje ha sido " + notification.State, new
                {
                    operation_name = operation.Description,
                    operation_image = operation.UrlLogo,
                    operation_room = room.First()
                },
                cancellationToken);
        }
    }
}