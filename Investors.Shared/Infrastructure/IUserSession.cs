namespace Investors.Shared.Infrastructure
{
    public interface IUserSession
    {
        string Token();
        void SetTokenVirtual(string token);
        string GetTokenVirtual();
        public const string UserVirtualCode = "850F91CD-A853-45BE-B677-0864CA160E0E";
        Guid Id { get; set; }
        string DisplayName { get; set; }
        string Name { get; set; }
        string SurName { get; set; }
        string Identification { get; set; }
        string Email { get; set; }
    }
}