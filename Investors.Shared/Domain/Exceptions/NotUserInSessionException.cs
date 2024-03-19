using System;
namespace Investors.Shared.Domain.Exceptions
{
    public sealed class NotUserInSessionException : AppException
    {
        public NotUserInSessionException(string message)
            : base($"Debe existir un usuario en sesión {message}.")
        {

        }
    }
}

