using Investors.Repository.EF.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investors.Repository.EF
{
    public static class DependencyContainer
    {
        private static void AddEntityFrameworkExtensions(this IServiceCollection services)
        {
            services.AddScoped<IEventsDbContext, EventsDbContext>();
        }

        public static void ConfigureRepositoryEf(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkExtensions();
        }
    }
}