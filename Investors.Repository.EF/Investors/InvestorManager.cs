using Investors.Kernel.Shared.Investors.Domain.Services;
using Investors.Repository.EF.Shared;
using MediatR;

namespace Investors.Repository.EF.Investors
{
    public class InvestorManager : IInvestorManager
    {
        private readonly Lazy<InvestorService> _investorService;

        public InvestorManager(InvestorsDbContext repositoryContext, ISender sender)
        {
            _investorService = new Lazy<InvestorService>(() =>
                new InvestorService(
                    investorRepository: new InvestorRepository(repositoryContext),
                    sender: sender));
        }

        public InvestorService Investors => _investorService.Value;
    }
}