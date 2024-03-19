namespace Investors.Kernel.Shared.Events.Application.Commands.Event.Create
{
    public record CreateEventRequest(
        string Description,
        int Order);
}