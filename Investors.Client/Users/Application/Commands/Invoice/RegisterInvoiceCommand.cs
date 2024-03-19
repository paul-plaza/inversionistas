using Investors.Client.Users.Domain.Services;
using MediatR;

namespace Investors.Client.Users.Application.Commands.Invoice
{
    public record RegisterInvoiceRequest(string Identification, DateTime InvoiceDate);

    public sealed record RegisterInvoiceCommand(Guid UserId, RegisterInvoiceRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterInvoiceHandler : IRequestHandler<RegisterInvoiceCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;
        public RegisterInvoiceHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterInvoiceCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.RegisterReceipt(request.UserId, request.Item.Identification, request.Item.InvoiceDate, cancellationToken);
            return user;
        }
    }
}