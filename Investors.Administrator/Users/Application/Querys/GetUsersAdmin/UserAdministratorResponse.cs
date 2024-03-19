namespace Investors.Administrator.Users.Application.Querys.GetUsersAdmin
{
    public record UserAdministratorResponse(
        int Id,
        string Email,
        List<OperationResponse> Operations,
        List<OptionsByUserResponse> Options);
}