using CSharpFunctionalExtensions;
using Investors.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace Investors.Azure.Identity.AzureIdentity
{
    public class AzureIdentity : IIdentityProvider
    {
        private readonly HttpClient _service;
        private readonly string _tenantId;
        private readonly string _routeDelete;
        private readonly string _clientId;
        private readonly string _clientSecret;
        public AzureIdentity(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _routeDelete = configuration["ConnectionMicrosoftGraph:DeleteUser"]!;
            _tenantId = configuration["AzureAdB2C:TenantId"]!;
            _clientId = configuration["AzureAdB2C:ClientUserAdmin"]!;
            _clientSecret = configuration["AzureAdB2C:ClientSecret"]!;
            _service = httpClientFactory.CreateClient("microsoftgraph");
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var app = ConfidentialClientApplicationBuilder
                .Create(_clientId)
                .WithClientSecret(_clientSecret)
                .WithAuthority($"https://login.microsoftonline.com/{_tenantId}/v2.0")
                .Build();

            var scopes = new string[]
            {
                "https://graph.microsoft.com/.default"
            };

            var authenticationResult = await app.AcquireTokenForClient(scopes)
                .ExecuteAsync();

            return authenticationResult.AccessToken;
        }

        public async Task<Result> UpdateUserAsync(Guid id, string displayName, string name, string surname)
        {
            var scopes = new[]
            {
                "https://graph.microsoft.com/.default"
            };

            var clientSecretCredential = new ClientSecretCredential(
                _tenantId, _clientId, _clientSecret);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var requestBody = new User
            {
                DisplayName = displayName,
                GivenName = name,
                Surname = surname
            };
            await graphClient.Users[id.ToString()].PatchAsync(requestBody);

            return Result.Success();
        }
        public async Task<Result> DeleteUserAsync(string accessToken, string userId)
        {

            _service.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _service.DefaultRequestHeaders.Add("x-ms-client-request-id", Guid.NewGuid().ToString());
            _service.DefaultRequestHeaders.Add("x-ms-correlation-id", Guid.NewGuid().ToString());

            // var deleteUserUrl = $"https://graph.microsoft.com/v1.0/{_b2CTenant}/users/{userId}";
            var response = await _service.DeleteAsync(_routeDelete + userId);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }
            return Result.Failure($"Error al eliminar el usuario. {response.StatusCode}");
        }
    }
}