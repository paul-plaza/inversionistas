using CSharpFunctionalExtensions;
using Investors.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    ///    Base controller for all controllers in the application.
    /// </summary>
    [Authorize]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Return an <see cref="IActionResult" /> con el resultado de la operación.
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }

        /// <summary>
        ///    Returns an <see cref="IActionResult" /> based on the <see cref="Result" />.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult FromResult(Result result)
        {
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
    }
}