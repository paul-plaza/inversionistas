using System.Text.Json.Serialization;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Application.Querys.UserInvestorById
{
    public record UserInvestorByIdResponse(
        Guid Id,
        bool IsInvestor,
        string Identification,
        string Name,
        string SurName);
}