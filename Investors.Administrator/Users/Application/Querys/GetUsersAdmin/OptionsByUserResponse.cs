namespace Investors.Administrator.Users.Application.Querys.GetUsersAdmin
{
    public record OptionsByUserResponse(
        int Id,
        string Name,
        string Description,
        string Route,
        string Icon,
        int Order
        );
}