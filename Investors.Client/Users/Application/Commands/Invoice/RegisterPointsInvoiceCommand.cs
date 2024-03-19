using Investors.Client.Users.Domain.BackgroundServices;
using MediatR;

namespace Investors.Client.Users.Application.Commands.Invoice
{
    public record RegisterPointsInvoiceCommand : IRequest<Result<ResponseDefault>>;


    public class RegisterPointsInvoiceHandler : IRequestHandler<RegisterInvoiceCommand, Result<ResponseDefault>>
    {
        private readonly IRegisterInvoiceDetailsJob _userManager;
        public RegisterPointsInvoiceHandler(IRegisterInvoiceDetailsJob userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoiceByReceipts = await _userManager.RegisterInvoiceByReceipts(cancellationToken);

            if (invoiceByReceipts.IsFailure)
            {
                return Result.Failure<ResponseDefault>(invoiceByReceipts.Error);
            }

            var invoicesForInvestors = await _userManager.RegisterInvoicesForInvestors(cancellationToken);

            if (invoiceByReceipts.IsFailure)
            {
                return Result.Failure<ResponseDefault>(invoicesForInvestors.Error);
            }

            return Result.Success(new ResponseDefault("Proceso ejecutado con exito"));
        }
    }
}