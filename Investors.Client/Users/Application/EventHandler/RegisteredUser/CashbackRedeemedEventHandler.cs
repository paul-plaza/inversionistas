using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Menu;
using Investors.Kernel.Shared.Operations.Application.Querys.Restaurant;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.RegisteredUser
{
    internal class CashbackRedeemedEventHandler : INotificationHandler<CashbackRedeemedEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        public CashbackRedeemedEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }
        public async Task Handle(CashbackRedeemedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Movement.RestaurantId != null)
            {
                var operation = await _sender.Send(new GetOperationAndRestaurantByIdQuery(notification.Movement.OperationId, notification.Movement.RestaurantId.Value), cancellationToken);

                var menusByRestaurant =
                    await _sender.Send(new MenuByOperationIdAndRestaurantIdQuery(notification.Movement.OperationId, notification.Movement.RestaurantId.Value), cancellationToken);

                var menusId = notification.Movement.CashbackDetails.Select(x => x.MenuId);

                var menus = menusByRestaurant.Value
                    .SelectMany(x => x.Menus)
                    .Where(x => menusId.Contains(x.Id)).ToList();

                string message = "Orden aprobada, disfruta de tu pedido";
                string templatePush = TemplateNotification.CashbackRedeemedPush;
                string templateEmail = TemplateNotification.CashbackRedeemedEmail;

                if (string.Equals(notification.State, "Rechazado"))
                {
                    message = "Su orden ha sido rechazada";
                    templatePush = TemplateNotification.CashbackRejectedPush;
                    templateEmail = TemplateNotification.CashbackRejectedEmail;
                }

                if (string.Equals(notification.State, "Cancelado"))
                {
                    message = "Su orden ha sido cancelada con éxito";
                    templatePush = TemplateNotification.CashbackCancelledPush;
                    templateEmail = TemplateNotification.CashbackCancelledEmail;
                }

                await _sender.Send(new RegisterNotificationCommand(
                    UserId: notification.Id, "Su orden ha sido " + notification.State,
                    SubTitle: null, NotificationType.Cashback,
                    Description: message,
                    operation.Value.OperationId,
                    UserInSession: notification.UserInSession), cancellationToken);

                await _managementNotifications.EditUserTag(notification.Id, new
                {
                    first_name = notification.UserName
                }, cancellationToken);

                //envio push
                await _managementNotifications.SentPushToUserId(
                    userId: notification.Id,
                    templateId: new Guid(templatePush),
                    cancellationToken: cancellationToken);

                //envio mail al usuario
                await _managementNotifications.SentEmailToUserId(
                    userId: notification.Id,
                    templateId: new Guid(templateEmail),
                    subject: "Su orden ha sido " + notification.State,
                    item: new
                    {
                        operation_name = operation.Value.Description,
                        operation_image = operation.Value.UrlLogo,
                        restaurant_name = operation.Value.Restaurants.First().Description,
                        products = menus
                    },
                    cancellationToken: cancellationToken);
            }
        }
    }
}