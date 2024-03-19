namespace Investors.Kernel.Shared.Events.Application.Commands.Event.Update
{
    public record UpdateEventRequest(
        int Id,
        string Description,
        int Order);
}