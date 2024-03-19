using System;
namespace Investors.Shared.Domain.Exceptions
{
    public sealed class ClientNullException : AppException
    {
        public ClientNullException(string message)
            : base($"Debe existir un cliente valido {message}.")
        {

        }
    }
}