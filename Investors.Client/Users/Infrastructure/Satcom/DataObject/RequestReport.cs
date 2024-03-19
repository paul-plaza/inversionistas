using System.Text.Json.Serialization;

namespace Investors.Client.Users.Infrastructure.Satcom.DataObject
{
    public record RequestReport
    {
        [JsonPropertyName("CodigoReporte")] public string ReportCode { get; init; }

        [JsonPropertyName("ResultadoGZipJSON")]
        public string GJson { get; init; }

        [JsonPropertyName("ResultadoMensaje")] public string Message { get; init; }
    }
}