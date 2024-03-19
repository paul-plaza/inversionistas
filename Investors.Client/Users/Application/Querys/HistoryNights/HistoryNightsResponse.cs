using System.Text.Json.Serialization;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Application.Querys.HistoryNights
{
    public record HistoryNightsResponse
    {
        public Guid Id { get; init; }

        public DateTime RequestDate { get; init; }

        public DateTime CheckIn { get; init; }

        public DateTime CheckOut { get; init; }

        public int TotalDays { get; init; }

        public int OperationId { get; set; }
        public string OperationName { get; init; }

        public string Image { get; set; }

        [JsonIgnore] public TransactionState Status { get; init; }

        [JsonPropertyName("status")] // Este es el nombre que se usará en la serialización.
        public string StatusString => Status.ToString();


    }
}