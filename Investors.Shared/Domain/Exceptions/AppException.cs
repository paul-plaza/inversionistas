using System;
using System.Globalization;

namespace Investors.Shared.Domain.Exceptions
{
    public class AppException : Exception
    {
        public AppException()
        {
        }

        public AppException(string message) : base(string.Format(CultureInfo.CurrentCulture, message))
        {
        }

        public AppException(string message, params object[] args) : base(
            string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

