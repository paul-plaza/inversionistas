using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investors.Administrator;

public static class DependencyContainer
{
    private static void AddDependencyMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AssemblyReference)));
    }
    public static void AddInvestorsAdministrators(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDependencyMediatR();
    }
}