namespace Investors.Administrator.Users.Application.Commands.CreateUser
{
    public record UserToCreateRequest(string Email, List<int> Operations, List<int> Options);
}