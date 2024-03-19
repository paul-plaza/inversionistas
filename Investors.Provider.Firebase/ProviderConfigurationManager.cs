using Google.Cloud.Storage.V1;
using Investors.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Investors.Provider.Firebase
{
    public class ProviderConfigurationManager : IProviderConfigurationManager
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;
        private readonly string _urlQr;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="storageClient"></param>
        /// <param name="firebaseOptions"></param>
        public ProviderConfigurationManager(IConfiguration configuration, StorageClient storageClient, IOptions<FirebaseConfig> firebaseOptions)
        {
            _storageClient = storageClient;
            _bucketName = firebaseOptions.Value.StorageBucket;
            _urlQr = configuration["StorageProfileLink:UrlQR"]!;
        }

        /// <summary>
        /// Carga el qr de un usuario al repositorio
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(string identification, Stream file, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            file.Seek(0, SeekOrigin.Begin);
            await file.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;
            await _storageClient.UploadObjectAsync(_bucketName, "users/" + identification + "/profile/qr.jpg", "image/jpeg", memoryStream, cancellationToken: cancellationToken);
            return $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/users%2F{identification}%2Fprofile%2Fqr.jpg?alt=media";
        }

        /// <summary>
        /// Obtengo la url del repositorio del usuario
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public string GetUrlQr(string identification)
        {
            return string.Format(_urlQr, identification);
        }
    }
}