using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Repository.EF.Shared;
using MediatR;

namespace Investors.Repository.EF.Operations
{

    public class OperationManager : IOperationManager
    {
        private readonly InvestorsDbContext _repositoryContext;

        private readonly Lazy<OperationService> _operationService;

        public OperationManager(InvestorsDbContext repositoryContext, ISender sender)
        {
            _repositoryContext = repositoryContext;

            _operationService = new Lazy<OperationService>(() => new OperationService(new OperationRepository(_repositoryContext), sender));
        }

        public OperationService Operations => _operationService.Value;
    }
}
