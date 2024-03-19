using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using Investors.Client.Users.Infrastructure.Satcom;
using Investors.Client.Users.Infrastructure.Satcom.DataObject;
using Investors.Shared.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investors.Satcom
{
    public class InvoiceManagement : IInvoiceManagement
    {
        private readonly HttpClient _service;
        private readonly IConfiguration _configuration;

        public InvoiceManagement(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _service = httpClientFactory.CreateClient("satcom");
        }


        public async Task<Result<List<ResponseInvoice>>> GetInvoices(
            int operationId,
            string userName,
            string password,
            RequestInvoiceFromDates request,
            CancellationToken cancellationToken)
        {
            _service.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password)));

            var requestInvoices = await _service.PostAsJsonAsync(_configuration["ConnectionSatcom:RouteGetInvoices"], request, cancellationToken);

            if (!requestInvoices.IsSuccessStatusCode)
                return Result.Failure<List<ResponseInvoice>>("Error al obtener facturas");

            var response = await requestInvoices.Content.ReadAsStringAsync(cancellationToken);

            var report = JsonSerializer.Deserialize<RequestReport>(response);

            //descomprimo el formato gzip guardada en report.GJson
            if (report?.GJson == null)
                return Result.Failure<List<ResponseInvoice>>(report?.Message);

            string json = JsonUtils.DecompressGZipJson(report.GJson);

            var invoices = JsonSerializer.Deserialize<List<ResponseInvoice>>(json);

            if (invoices is null)
            {
                return Result.Failure<List<ResponseInvoice>>("Error al obtener facturas");
            }

            invoices.ForEach(x => x.OperationId = operationId);

            return Result.Success(invoices);
        }
    }
}