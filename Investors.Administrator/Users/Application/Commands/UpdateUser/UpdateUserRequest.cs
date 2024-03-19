namespace Investors.Administrator.Users.Application.Commands.UpdateUser
{
    public record UpdateUserRequest(int Id, string Email, List<int> Operations, List<int> Options);
}