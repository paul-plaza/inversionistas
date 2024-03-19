using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Investors.Application.Commands.InvestorManagement
{
    public sealed record InvestorManagementCommand(Guid UserId, List<InvestorManagementRequest> Investors) : IRequest<Result<ResponseDefault>>;

    public class InvestorManagementHandler : IRequestHandler<InvestorManagementCommand, Result<ResponseDefault>>
    {
        private readonly IInvestorManager _investorManager;
        public InvestorManagementHandler(IInvestorManager investorManager)
        {
            _investorManager = investorManager;
        }
        public async Task<Result<ResponseDefault>> Handle(InvestorManagementCommand request, CancellationToken cancellationToken)
        {
            var user = await _investorManager.Investors.RegisterInvestor(request.Investors, request.UserId, cancellationToken);
            return user;
        }
    }
}