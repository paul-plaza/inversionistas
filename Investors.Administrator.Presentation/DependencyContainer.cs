using Investors.Administrator.Shared.Infrastructure;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Azure.Identity.AzureIdentity;
using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.BackgroundServices;
using Investors.Client.Users.Domain.Repositories;
using Investors.Client.Users.Domain.Services;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Kernel.Shared.Investors.Domain.Services;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Kernel.Shared.Shared.Infrastructure;
using Investors.OneSignal;
using Investors.Repository.EF.Events;
using Investors.Repository.EF.Investors;
using Investors.Repository.EF.Operations;
using Investors.Repository.EF.Shared;
using Investors.Repository.EF.UserAdministrators;
using Investors.Repository.EF.Users;
using Investors.Shared.Infrastructure;
using Investors.Shared.Presentation.Session;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Investors.Administrator.Presentation
{
    public static class DependencyContainer
    {
        /// <summary>
        ///   Configuro AzureAD
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void ConfigureAuthenticationAzureAd(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));
        }

        private static void AddRepositoryOnlyRead(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryAdminForReadManager, RepositoryForReadManager>();
            services.AddScoped<IRepositoryClientForReadManager, RepositoryForReadManager>();
            services.AddScoped<IRepositorySharedForReadManager, RepositoryForReadManager>();
            services.AddScoped<IRepositoryOptionForRead, RepositoryOptionFor>();
            services.AddScoped<IRepositoryUserAdministratorForRead, RepositoryUserAdministratorFor>();
        }

        private static void AddNotificationManagement(this IServiceCollection services)
        {
            services.AddScoped<IManagementNotifications, ManagementNotifications>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IOperationManager, OperationManager>();
            services.AddScoped<IInvestorManager, InvestorManager>();
            services.AddScoped<IUserWriteRepository, UserRepository>();
            services.AddScoped<IUserAdministratorWriteRepository, RepositoryUserAdministratorFor>();
            services.AddScoped<IIdentityProvider, AzureIdentity>();
            services.AddScoped<IEventDetailManager, EventManager>();
        }

        /// <summary>
        /// Configuro los servicios de la capa de presentación
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureAdministratorPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthenticationAzureAd(configuration);
            services.AddRepositoryOnlyRead();
            services.AddServices();
            services.AddNotificationManagement();
        }
    }
}