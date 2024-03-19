using System.Text.Json.Serialization;

namespace Investors.Client.Users.Infrastructure.Satcom.DataObject
{
    public record RequestInvoiceFromDates
    {

        [JsonPropertyName("NombreReporte")] public string ReportName { get; } = "RS001";
        [JsonPropertyName("CodigoReporte")] public string ReportCode { get; } = "RS001";
        [JsonPropertyName("FechaInicio")] public string From { get; init; }
        [JsonPropertyName("FechaFin")] public string To { get; init; }

        [JsonPropertyName("CodigoEstablecimiento")]
        public string SpotSectionCode { get; } = string.Empty;

        [JsonPropertyName("CodigoPunto")] public string SpotCode { get; } = string.Empty;
    }
}