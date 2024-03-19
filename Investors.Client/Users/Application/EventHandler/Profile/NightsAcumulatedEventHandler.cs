using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.ProfileEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.Profile
{
    internal class NightsAccumulatedEventHandler : INotificationHandler<NightsAccumulatedEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        public NightsAccumulatedEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }
        public async Task Handle(NightsAccumulatedEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RegisterNotificationCommand(
                    notification.UserId,
                    notification.NameEvent,
                    null,
                    NotificationType.Nights,
                    $"Felicidades se han agregado {notification.NumberNights} noches de su factura {notification.InvoiceDocument} a sus noches acumuladas.",
                    notification.OperationId,
                    notification.UserInSession)
                , cancellationToken);

            //envio push
            await _managementNotifications.SentPushToUserId(
                notification.UserId,
                new Guid(TemplateNotification.NightsAccumulatedPush),
                cancellationToken);

            //envio mail al usuario
            await _managementNotifications.SentEmailToUserId(
                notification.UserId,
                new Guid(TemplateNotification.NightsAccumulatedEmail),
                $"Felicidades se han agregado {notification.NumberNights} noches de su factura {notification.InvoiceDocument} en total a sus noches acumuladas, revisalas en tu app",
                cancellationToken);
        }
    }

}