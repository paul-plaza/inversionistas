using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs;
using Investors.Kernel.Shared.Operations.Application.Querys.Restaurant;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.RegisteredUser
{
    internal class RegisteredRedemptionCashbackEventHandler : INotificationHandler<RegisteredRedemptionCashbackEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        public RegisteredRedemptionCashbackEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }
        public async Task Handle(RegisteredRedemptionCashbackEvent notification, CancellationToken cancellationToken)
        {
            var operationsWithRestaurants = await _sender.Send(new GetOperationAndRestaurantByIdQuery(notification.OperationId, notification.RestaurantId), cancellationToken);

            if (operationsWithRestaurants.IsFailure)
            {
                return;
            }

            //obtengo url de api de administrador para aceptar o rechazar la orden
            var catalog = await _sender.Send(new CatalogUrlAdministratorApiQuery(), cancellationToken);

            await _sender.Send(new RegisterNotificationCommand(notification.Id,
                "Felicitaciones orden creada",
                null, NotificationType.Cashback,
                "Hemos recibido su orden pronto le atenderemos",
                notification.OperationId,
                notification.UserInSession), cancellationToken);

            await _managementNotifications.EditUserTag(notification.Id, new
            {
                first_name = notification.UserName
            }, cancellationToken);

            //envio push
            await _managementNotifications.SentPushToUserId(
                notification.Id,
                new Guid(TemplateNotification.ShoppingCartPush),
                cancellationToken);

            //envio mail al usuario
            await _managementNotifications.SentEmailToUserId(
                notification.Id,
                new Guid(TemplateNotification.ShoppingCartEmailUser),
                "Nuevo Orden de compras", notification.Email, new
                {
                    operation_name = operationsWithRestaurants.Value.Description,
                    operation_image = operationsWithRestaurants.Value.UrlLogo,
                    restaurant_name = operationsWithRestaurants.Value.Restaurants.First().Description,
                    products = notification.Menu
                },
                cancellationToken);

            //envio mail al cajero
            await _managementNotifications.SentEmailToAdmin(
                new Guid(TemplateNotification.ShoppingCartEmailAdmin),
                "Nuevo Orden de compras de inversionista", operationsWithRestaurants.Value.Restaurants.First().Email, new
                {
                    operation_name = operationsWithRestaurants.Value.Description,
                    operation_image = operationsWithRestaurants.Value.UrlLogo,
                    restaurant_name = operationsWithRestaurants.Value.Restaurants.First().Description,
                    products = notification.Menu,
                    url_accept = $"{catalog.Value}{notification.Id}/cashback/{notification.MovementId}/accept",
                    url_reject = $"{catalog.Value}{notification.Id}/cashback/{notification.MovementId}/reject",
                    user_name = notification.UserName,
                    user_identification = notification.Identification
                },
                cancellationToken);
        }
    }
}