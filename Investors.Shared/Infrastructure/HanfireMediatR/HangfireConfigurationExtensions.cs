using Hangfire;
using Newtonsoft.Json;

namespace Investors.Shared.Infrastructure.HanfireMediatR
{
    public static class HangfireConfigurationExtensions
    {
        public static void UseMediatR(this IGlobalConfiguration configuration)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            configuration.UseSerializerSettings(jsonSettings);

        }
    }
}

