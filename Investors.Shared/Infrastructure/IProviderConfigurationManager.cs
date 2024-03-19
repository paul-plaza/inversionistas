namespace Investors.Shared.Infrastructure
{
    public interface IProviderConfigurationManager
    {
        Task<string> UploadFile(string identification, Stream file, CancellationToken cancellationToken);
        string GetUrlQr(string identification);
    }
}
