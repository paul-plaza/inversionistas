using System.Net.Http.Json;
using CSharpFunctionalExtensions;
using Investors.Client.Users.Infrastructure.Notifications;
using Microsoft.Extensions.Configuration;

namespace Investors.OneSignal
{
    public class ManagementNotifications : IManagementNotifications
    {
        private readonly HttpClient _service;
        private readonly IConfiguration _configuration;

        public ManagementNotifications(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _service = httpClientFactory.CreateClient("oneSignal");
        }

        public async Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, string to, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                include_external_user_ids = new List<string>
                {
                    userId.ToString()
                },
                channel_for_external_user_ids = Channels.Email,
                template_id = templateId,
                email_subject = subject,
                email_from_name = _configuration["ConnectionOneSignal:EmailFromName"],
                include_email_tokens = new List<string>
                {
                    to
                }
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }

        public async Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                include_external_user_ids = new List<string>
                {
                    userId.ToString()
                },
                channel_for_external_user_ids = Channels.Email,
                template_id = templateId,
                email_subject = subject,
                email_from_name = _configuration["ConnectionOneSignal:EmailFromName"]
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }

        public async Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, string to, object item, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                include_external_user_ids = new List<string>
                {
                    userId.ToString()
                },
                channel_for_external_user_ids = Channels.Email,
                template_id = templateId,
                email_subject = subject,
                email_from_name = _configuration["ConnectionOneSignal:EmailFromName"],
                include_email_tokens = new List<string>
                {
                    to
                },
                custom_data = item
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }

        public async Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, object item, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                include_external_user_ids = new List<string>
                {
                    userId.ToString()
                },
                channel_for_external_user_ids = Channels.Email,
                template_id = templateId,
                email_subject = subject,
                email_from_name = _configuration["ConnectionOneSignal:EmailFromName"],
                custom_data = item
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }

        public async Task<Result> SentPushToUserId(Guid userId, Guid templateId, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                include_external_user_ids = new List<string>
                {
                    userId.ToString()
                },
                channel_for_external_user_ids = Channels.Push,
                template_id = templateId
            }, cancellationToken);

            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);

        }
        public async Task<Result> SentPushToUserId(Guid userId, Guid templateId, object item, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                include_external_user_ids = new List<string>
                {
                    userId.ToString()
                },
                channel_for_external_user_ids = Channels.Push,
                template_id = templateId,
                custom_data = item
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }

        public async Task<Result> EditUserTag(Guid userId, object item, CancellationToken cancellationToken)
        {
            var response = await _service.PutAsJsonAsync(string.Format(_configuration["ConnectionOneSignal:CreateTags"]!, userId), new
            {
                tags = item
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }

        public async Task<Result> SentEmailToAdmin(Guid templateId, string subject, string to, object item, CancellationToken cancellationToken)
        {
            var response = await _service.PostAsJsonAsync(_configuration["ConnectionOneSignal:CreateNotification"], new
            {
                app_id = _configuration["ConnectionOneSignal:AppId"],
                channel_for_external_user_ids = Channels.Email,
                template_id = templateId,
                email_subject = subject,
                email_from_name = _configuration["ConnectionOneSignal:EmailFromName"],
                include_email_tokens = new List<string>
                {
                    to
                },
                custom_data = item
            }, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return response.IsSuccessStatusCode ? Result.Success() : Result.Failure(result);
        }
    }
}