using CSharpFunctionalExtensions;

namespace Investors.Shared.Infrastructure
{
    public interface IIdentityProvider
    {
        Task<string> GetAccessTokenAsync();
        Task<Result> DeleteUserAsync(string accessToken, string userId);
    }
}
