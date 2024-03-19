using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.ProfileEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.Profile
{

    internal class CashbackAccumulatedEventHandler : INotificationHandler<CashbackAccumulatedEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        public CashbackAccumulatedEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }
        public async Task Handle(CashbackAccumulatedEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RegisterNotificationCommand(
                    notification.UserId,
                    notification.NameEvent,
                    null,
                    NotificationType.Cashback,
                    $"Felicidades se han agregado {notification.TotalPoints} puntos de su factura {notification.InvoiceDocument}",
                    notification.OperationId,
                    notification.UserInSession)
                , cancellationToken);

            //envio push
            await _managementNotifications.SentPushToUserId(
                notification.UserId,
                new Guid(TemplateNotification.CashbackAccumulatedPush),
                cancellationToken);

            //envio mail al usuario
            await _managementNotifications.SentEmailToUserId(
                notification.UserId,
                new Guid(TemplateNotification.CashbackAccumulatedEmail),
                $"Felicidades se han agregado {notification.TotalPoints} puntos de su factura {notification.InvoiceDocument} para que puedas canjear, revisalos en su app",
                cancellationToken);
        }
    }
}