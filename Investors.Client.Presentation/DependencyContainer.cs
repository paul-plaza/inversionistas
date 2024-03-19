using Investors.Azure.Identity.AzureIdentity;
using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.BackgroundServices;
using Investors.Client.Users.Domain.Repositories;
using Investors.Client.Users.Domain.Services;
using Investors.Client.Users.Infrastructure.Notifications;
using Investors.Client.Users.Infrastructure.Satcom;
using Investors.Client.Users.Repositories;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Kernel.Shared.Investors.Domain.Services;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Kernel.Shared.Shared.Infrastructure;
using Investors.OneSignal;
using Investors.Repository.EF.Events;
using Investors.Repository.EF.Investors;
using Investors.Repository.EF.Operations;
using Investors.Repository.EF.Shared;
using Investors.Repository.EF.Users;
using Investors.Satcom;
using Investors.Shared.Infrastructure;
using Investors.Shared.Presentation.Session;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Investors.Client.Presentation
{
    public static class DependencyContainer
    {
        /// <summary>
        ///   Configuro AzureB2C
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void ConfigureAuthenticationAzureB2C(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAdB2C"));
        }

        private static void AddRepositoryOnlyRead(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryClientForReadManager, RepositoryForReadManager>();
            services.AddScoped<IRepositorySharedForReadManager, RepositoryForReadManager>();
        }

        private static void AddNotificationManagement(this IServiceCollection services)
        {
            services.AddScoped<IManagementNotifications, ManagementNotifications>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRegisterInvoiceDetailsJob, RegisterInvoiceDetailsJob>();
            services.AddScoped<IInvoiceManagement, InvoiceManagement>();
            services.AddScoped<IInvestorManager, InvestorManager>();
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IOperationManager, OperationManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserWriteRepository, UserRepository>();
            services.AddScoped<IReceiptWriteRepository, ReceiptRepository>();
            services.AddScoped<IIdentityProvider, AzureIdentity>();
            services.AddScoped<IEventDetailManager, EventManager>();
        }

        /// <summary>
        /// Configuro los servicios de la capa de presentación
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureClientPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthenticationAzureB2C(configuration);
            services.AddRepositoryOnlyRead();
            services.AddServices();
            services.AddNotificationManagement();
        }
    }
}