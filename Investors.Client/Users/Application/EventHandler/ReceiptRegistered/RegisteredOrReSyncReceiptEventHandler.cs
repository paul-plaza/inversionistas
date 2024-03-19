using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.ReceiptRegistered
{
    internal class RegisteredOrReSyncReceiptEventHandler : INotificationHandler<RegisterOrReSyncReceiptEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;

        public RegisteredOrReSyncReceiptEventHandler(ISender sender, IManagementNotifications managementNotifications)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
        }

        public async Task Handle(RegisterOrReSyncReceiptEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RegisterNotificationCommand(
                    notification.UserId,
                    "Registro factura de referido",
                    null,
                    NotificationType.News,
                    $"Ingreso de factura de referido por acumulación de puntos ingresado correctamente, Identificación: {notification.Identification}, Fecha: {notification.DateInvoice.ToString()}. En un plazo de 48 horas su solicitud será revisada y procesada.",
                    null,
                    notification.UserInSession)
                , cancellationToken);

            await _managementNotifications.SentPushToUserId(
                notification.UserId,
                new Guid(TemplateNotification.RegisteredReceiptPush),
                cancellationToken);

            await _managementNotifications.SentEmailToUserId(
                notification.UserId,
                new Guid(TemplateNotification.RegisteredReceiptEmail),
                "Registro de referido SIXSTAR MEMBERS",
                new
                {
                    userName = notification.UserName,
                    email = notification.Email,
                    identification = notification.Identification,
                    date = notification.DateInvoice.ToString(),
                },
                cancellationToken);

            var emailAdministrator = await _sender.Send(new CatalogEmailAdministratorQuery(), cancellationToken);

            if (emailAdministrator.IsSuccess)
            {
                await _managementNotifications.SentEmailToAdmin(
                    new Guid(TemplateNotification.RegisteredReceiptEmail),
                    "Registro de referido SIXSTAR MEMBERS",
                    emailAdministrator.Value,
                    new
                    {
                        userName = notification.UserName,
                        email = notification.Email,
                        identification = notification.Identification,
                        date = notification.DateInvoice.ToString(),
                    },
                    cancellationToken);
            }


        }
    }
}