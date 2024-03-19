using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investors.Kernel.Shared;

public static class DependencyContainer
{
    private static void AddDependencyMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AssemblyReference)));
    }
    public static void AddInvestorsKernelShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDependencyMediatR();
    }
}