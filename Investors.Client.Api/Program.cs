using System.Net.Mime;
using System.Text.Json.Serialization;
using Hangfire;
using Investors.Client.Api.BackgroundServices;
using Investors.Client.Api.Extensions;
using Investors.Client.Presentation;
using Investors.Client.Shared;
using Investors.Kernel.Shared;
using Investors.Provider.Firebase;
using Investors.Repository.EF;
using Investors.Shared.Infrastructure;
using Investors.Shared.Infrastructure.HanfireMediatR;
using Investors.Shared.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//obtengo la propiedad Provider de configuration
var provider = builder.Configuration.GetSection("Provider").Value;

//si es sqlite
if (provider == "Sqlite")
{
    builder.Services.ConfigureSqliteContext(builder.Configuration);
}

//si es sqlserver
if (provider == "SqlServer")
{
    builder.Services.ConfigureSqlContext(builder.Configuration);
}

builder.Services.ConfigureHangFireClient(builder.Configuration);

builder.Services.ConfigureHangFireServer();

builder.Services.ConfigureCors();

builder.Services.ConfigureClientPresentation(builder.Configuration);

builder.Services.AddInvestorsClient(builder.Configuration);

builder.Services.AddInvestorsKernelShared(builder.Configuration);

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureProviderService();
builder.Services.ConfigureFireBase(builder.Configuration);

builder.Services.ConfigureRepositoryEf(builder.Configuration);

builder.Services.AddControllers(config =>
    {
        //indico que debe restringir las cabeceras aceptadas
        config.RespectBrowserAcceptHeader = true;
        //indico que devuelva un error si el cliente trata de negociar con un tipo de dato que no soporta el api
        //puede devolver un 406 Not Acceptable
        config.ReturnHttpNotAcceptable = true;

        config.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml));
        config.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml));
    })
    //agrego configuracion para que pueda leer xml
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(AssemblyReference).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureProvidersApis(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureSwagger(builder.Configuration);

var app = builder.Build();

//Configuro control global de excepciones
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthClientId(builder.Configuration["AzureADB2CSwagger:OpenIdClientId"]);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Hangfire Inversionistas",
    IgnoreAntiforgeryToken = true,
    Authorization = new[]
    {
        new HangfireAuthorizationFilter("xxx", "xxxx")
    }
});

app.UseCors("CorsPolicy");

app.MapControllers();

BackgroundJobServices.ConfigureJobs();
app.Run();