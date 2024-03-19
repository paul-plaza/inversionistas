using System.Text.Json.Serialization;

namespace Investors.Client.Users.Infrastructure.Satcom.DataObject
{
    public record ResponseInvoice
    {
        [JsonPropertyName("Identificacion")] public string Identification { get; init; }
        [JsonPropertyName("RazonSocial")] public string BusinessName { get; init; }
        [JsonPropertyName("Estab")] public string Institution { get; init; }
        [JsonPropertyName("Punto")] public string Spot { get; init; }
        [JsonPropertyName("Documento")] public string Document { get; init; }
        [JsonPropertyName("FechaEmision")] public DateTime DateOfIssue { get; init; }
        [JsonPropertyName("Total")] public double Total { get; init; }

        [JsonPropertyName("DescripcionEstado")]
        public string DescriptionState { get; init; }

        [JsonPropertyName("Grupo")] public string Group { get; init; }
        [JsonPropertyName("Ambiente")] public object Environment { get; init; }
        [JsonPropertyName("Subtotal")] public double Subtotal { get; init; }
        [JsonPropertyName("Fecha_Llegada")] public string ArrivalDate { get; init; }
        [JsonPropertyName("Fecha_Salida")] public string DepartureDate { get; init; }

        public int ReceiptId { get; set; }

        public int TransactionType { get; set; }

        public int OperationId { get; set; }

        public int TotalDays
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ArrivalDate) && !string.IsNullOrWhiteSpace(DepartureDate))
                {
                    var x = DateTime.TryParse(ArrivalDate, out DateTime fromDate);
                    var y = DateTime.TryParse(DepartureDate, out DateTime fromDateDeparture);
                    if (x && y)
                    {
                        return fromDate <= fromDateDeparture ? fromDateDeparture.Subtract(fromDate).Days : 0;
                    }
                }
                return 0;
            }
        }
    }
}