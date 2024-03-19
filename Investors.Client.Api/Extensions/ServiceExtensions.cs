using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Hangfire;
using Hangfire.MemoryStorage;
using Investors.LoggerService;
using Investors.Provider.Firebase;
using Investors.Repository.EF.Shared;
using Investors.Shared.Infrastructure;
using Investors.Shared.Infrastructure.HanfireMediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;

namespace Investors.Client.Api.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        ///     Configuration de CORS
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                        //.WithOrigins("https://example.com")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InvestorsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure();
                        })
                    .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning)));
        }

        public static void ConfigureSqliteContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InvestorsDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureProviderService(this IServiceCollection services)
        {
            services.AddSingleton<IProviderConfigurationManager, ProviderConfigurationManager>();
        }

        /// <summary>
        /// Configuracion de cliente hangfire
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureHangFireClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage()
                .UseMediatR());
        }

        public static void ConfigureProvidersApis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("satcom", c =>
            {
                c.BaseAddress = new Uri(configuration["ConnectionSatcom:DefaultConnection"]!);
                c.Timeout = TimeSpan.FromMinutes(5);
            });

            services.AddHttpClient("microsoftgraph", c =>
            {
                c.BaseAddress = new Uri(configuration["ConnectionMicrosoftGraph:DefaultConnection"]!);
            });

            services.AddHttpClient("oneSignal", c =>
            {
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    configuration["ConnectionOneSignal:ApiKey"]);
                c.BaseAddress = new Uri(configuration["ConnectionOneSignal:DefaultConnection"]!);
            });
        }



        public static void ConfigureHangFireServer(this IServiceCollection services)
        {

            services.AddHangfireServer();
        }

        /// <summary>
        /// Configura swagger con AzureB2C
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Inversionistas Tryp",
                    Version = "v1"
                });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["AzureADB2CSwagger:AuthorizationUrl"]!),
                            TokenUrl = new Uri(configuration["AzureADB2CSwagger:TokenUrl"]!),
                            Scopes = new Dictionary<string, string>
                            {
                                {
                                    configuration["AzureADB2CSwagger:ApiReadScope"]!, "Permitir acceso a Lectura"
                                },
                                {
                                    configuration["AzureADB2CSwagger:ApiWriteScope"]!, "Permitir acceso a escritura"
                                }
                            }
                        }
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new[]
                        {
                            configuration["AzureADB2CSwagger:ApiReadScope"]!, configuration["AzureADB2CSwagger:ApiWriteScope"]
                        }
                    }
                });

                var xmlFile = $"Investors.Client.Presentation.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}