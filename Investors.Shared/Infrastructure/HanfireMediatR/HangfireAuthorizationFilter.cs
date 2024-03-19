using System.Net;
using System.Text;
using Hangfire.Dashboard;

namespace Investors.Shared.Infrastructure.HanfireMediatR
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private string _username;
        private string _password;

        public HangfireAuthorizationFilter(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                string decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];

                if (username.Equals(_username, StringComparison.InvariantCultureIgnoreCase) && password.Equals(_password))
                {
                    return true;
                }
            }

            httpContext.Response.Headers["WWW-Authenticate"] = "Basic";
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return false;
        }
    }
}