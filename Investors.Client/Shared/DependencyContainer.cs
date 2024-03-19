using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investors.Client.Shared;

public static class DependencyContainer
{
    private static void AddDependencyMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AssemblyReference)));
    }
    public static void AddInvestorsClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDependencyMediatR();
    }
}