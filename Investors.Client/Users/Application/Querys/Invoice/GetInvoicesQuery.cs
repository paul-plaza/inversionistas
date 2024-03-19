using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Specifications;
using MediatR;

namespace Investors.Client.Users.Application.Querys.Invoice
{

    public record GetInvoicesQuery() : IRequest<Result<List<InvoicesResponse>>>;

    public class RoomsByOperationIdHandler : IRequestHandler<GetInvoicesQuery, Result<List<InvoicesResponse>>>
    {
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public RoomsByOperationIdHandler(IRepositoryClientForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<List<InvoicesResponse>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _repositoryClient.Invoices.ListAsync(new GetInvoicesSpecs(), cancellationToken);

            if (!invoices.Any())
            {
                return Result.Failure<List<InvoicesResponse>>("Facturas no encontradas");
            }
            return Result.Success(invoices.Select(invoice => new InvoicesResponse(
                    Id: invoice.Id,
                    Number: invoice.Number,
                    Identification: invoice.Identification))
                .ToList());
        }
    }
}