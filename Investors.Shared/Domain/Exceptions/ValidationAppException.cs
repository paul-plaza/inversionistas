namespace Investors.Shared.Domain.Exceptions
{
    public sealed class ValidationAppException : AppException
    {

        public ValidationAppException(IReadOnlyDictionary<string, string[]> errors)
            : base("One or more validation errors occurred")
        {
            Errors = errors;
        }
        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}