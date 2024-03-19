using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace Investors.Shared.Domain.ValueObjects
{
    public record Email : IValueObject
    {
        public const int MaxLength = 150;

        private Email(string value)
        {
            Value = value;
        }
        public string Value { get; }

        public static Result<Email> Create(string email)
        {
            Guard.Against.NullOrWhiteSpace(email, "Email should not be empty");

            if (email.Length > 150)

                return Result.Failure<Email>("Email is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Email is invalid");

            return Result.Success(new Email(email.ToLower()));
        }

        public static explicit operator Email(string email)
        {
            return Create(email).Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}