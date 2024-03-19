namespace Investors.Client.Users.Application.Commands.UpdateUser
{
    public record UpdateUserRequest(
        string Identification,
        string DisplayName,
        Guid Id,
        string Name,
        string Surname);
}