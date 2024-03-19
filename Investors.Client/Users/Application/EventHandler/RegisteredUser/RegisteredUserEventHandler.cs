using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Shared.Infrastructure;
using Investors.Shared.Infrastructure.OneSignal;
using MediatR;

namespace Investors.Client.Users.Application.EventHandler.RegisteredUser
{
    internal class RegisteredUserEventHandler : INotificationHandler<RegisteredUserEvent>
    {
        private readonly ISender _sender;
        private readonly IManagementNotifications _managementNotifications;
        private readonly IProviderConfigurationManager _provider;

        public RegisteredUserEventHandler(ISender sender, IManagementNotifications managementNotifications, IProviderConfigurationManager provider)
        {
            _sender = sender;
            _managementNotifications = managementNotifications;
            _provider = provider;
        }
        public async Task Handle(RegisteredUserEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(new RegisterNotificationCommand(notification.Id, "Usuario creado",
                null, NotificationType.General, "Bienvenido, su usuario se ha creado con éxito", null, notification.UserInSession), cancellationToken);

            //Creo Imagen QR
            // var qrImage = _qrCreator.GenerateQr(notification.Identification);
            // using var imageStream = new MemoryStream();
            // qrImage.Encode(SKEncodedImageFormat.Jpeg, 100)
            //     .SaveTo(imageStream);

            //Subo a firestore
            // await _provider.UploadFile(notification.Identification, imageStream, cancellationToken);

            //Agrego tag a usuario de one signal para que tome esos datos en template
            await _managementNotifications.EditUserTag(notification.Id, new
            {
                first_name = notification.Name
            }, cancellationToken);


            //envio push
            await _managementNotifications.SentPushToUserId(
                notification.Id,
                new Guid(TemplateNotification.WelcomePush),
                cancellationToken);
        }
    }
}