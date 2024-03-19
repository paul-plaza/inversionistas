using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investors.Provider.Firebase
{
    public static class DependencyContainer
    {
        public static void ConfigureFireBase(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection firebaseConfigSection = configuration.GetSection("Firebase");
            services.Configure<FirebaseConfig>(firebaseConfigSection);

            // Obtener la configuración de Firebase y crear las opciones de Firebase
            var firebaseConfig = firebaseConfigSection.Get<FirebaseConfig>();
            var firebaseOptions = new FirebaseAdmin.AppOptions
            {
                Credential = GoogleCredential.FromFile(firebaseConfig.CredentialsFilePath),
                ProjectId = firebaseConfig.ProjectId,
            };
            FirebaseApp.Create(firebaseOptions);

            // Agregar el cliente de Google Cloud Storage y el StorageBucket
            services.AddSingleton(x => StorageClient.Create(firebaseOptions.Credential));
            services.AddSingleton(firebaseConfig.StorageBucket);
        }
    }
}
