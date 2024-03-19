namespace Investors.Client.Users.Application.Querys.HistoryCashbackDetail
{
    public record HistoryCashbackDetailResponse
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int Points { get; init; }
    }
}