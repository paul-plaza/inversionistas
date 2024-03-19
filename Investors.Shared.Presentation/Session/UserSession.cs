using Investors.Shared.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Investors.Shared.Presentation.Session
{
    public class UserSession : IUserSession
    {

        public const string UserVirtualCode = "850F91CD-A853-45BE-B677-0864CA160E0E";
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static string _tokenUserVirtual { get; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Identification { get; set; }

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext?.User;

            if (user != null && user.Identity!.IsAuthenticated)
            {
                DisplayName = user.Claims.FirstOrDefault(x => x.Type == "name")?.Value!;
                Identification = user.Claims.FirstOrDefault(x => x.Type == "extension_Identification")?.Value!;
                Name = user.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value!;
                SurName = user.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value!;
                Id = Guid.Parse(user.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
                Email = user.Claims.FirstOrDefault(x => x.Type == "emails")?.Value!;

                if (string.IsNullOrEmpty(Email))
                {
                    Email = user.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value!;
                }

                if (string.IsNullOrEmpty(Email))
                {
                    Email = user.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value!;
                }

            }
        }

        public void SetTokenVirtual(string token)
        {
            //TODO: Implementar el metodo SetTokenVirtual
        }

        public string GetTokenVirtual()
        {
            return _tokenUserVirtual;
        }

        public string Token()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is null)
            {
                return string.Empty;
            }

            var token = httpContext.Request.Headers["Authorization"].ToString();

            return token.Replace("Bearer", string.Empty).Trim();
        }
    }
}