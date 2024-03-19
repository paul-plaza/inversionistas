namespace Investors.Administrator.Options.Application.Querys.GetOptions
{
    public record OptionResponse(
        int Id,
        string Name,
        string Description,
        string Route,
        string Icon,
        int Order
        );
}