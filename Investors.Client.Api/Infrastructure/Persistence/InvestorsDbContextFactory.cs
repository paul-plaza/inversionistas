using Investors.Repository.EF.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Investors.Client.Api.Infrastructure.Persistence
{
    internal class InvestorsDbContextFactory : IDesignTimeDbContextFactory<InvestorsDbContext>
    {
        //add-migration "v1" -p ActionPlan.WebApi -c ActionPlanDbContext -o Infrastructure/Persistence/Migrations -s ActionPlan.WebApi

        public InvestorsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //obtengo la propiedad Provider de configuration
            var provider = configuration.GetSection("Provider").Value;

            DbContextOptionsBuilder<InvestorsDbContext> builder = null;
            if (provider == "SqlServer")
            {
                builder = new DbContextOptionsBuilder<InvestorsDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                        b => b.MigrationsAssembly("Investors.Client.Api"));
            }

            if (provider == "Sqlite")
            {
                builder = new DbContextOptionsBuilder<InvestorsDbContext>()
                    .UseSqlite(configuration.GetConnectionString("SqliteConnection"),
                        b => b.MigrationsAssembly("Investors.Client.Api"));
            }

            return new InvestorsDbContext(builder!.Options, null, null);
        }
    }
}