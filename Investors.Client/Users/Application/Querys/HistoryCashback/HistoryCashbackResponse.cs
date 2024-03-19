using System.Text.Json.Serialization;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Application.Querys.HistoryCashback
{
    public record HistoryCashbackResponse
    {
        public Guid Id { get; init; }

        public DateTime Date { get; init; }

        public int Amount { get; init; }

        public int TotalItems { get; init; }

        public int OperationId { get; set; }
        public string OperationName { get; init; }

        public int RestaurantId { get; set; }

        public string Image { get; set; }
        public string RestaurantName { get; init; }

        [JsonIgnore] public TransactionState Status { get; init; }

        [JsonPropertyName("status")] // Este es el nombre que se usará en la serialización.
        public string StatusString => Status.ToString();


    }
}