using System.Net;
using System.Net.Mime;
using Investors.Shared.Domain.Exceptions;
using Investors.Shared.Infrastructure;
using Investors.Shared.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Investors.Shared.Presentation.Extensions
{
    /// <summary>
    ///    Extensiones para el manejo de excepciones
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        ///   Configura el manejador de excepciones
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            NoContentException => StatusCodes.Status204NoContent,
                            NotAcceptableException => StatusCodes.Status406NotAcceptable,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                            UserForbiddenException => StatusCodes.Status403Forbidden,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Algo salio mal: {contextFeature.Error}");
                        await context.Response.WriteAsync(Envelope.Error(contextFeature.Error.Message).ToJson());
                    }
                });
            });
        }
    }
}