namespace Investors.Kernel.Shared.Operations.Application.Commands.Menu
{
    public class MenuRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
    }
}
