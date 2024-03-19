using Investors.Administrator.Reports.Repositories;
using Investors.Administrator.Shared.Infrastructure;
using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.Repositories;
using Investors.Client.Users.Repositories;
using Investors.Kernel.Shared.Catalogs.Repositories;
using Investors.Kernel.Shared.Events.Repositories;
using Investors.Kernel.Shared.Investors.Domain.Repositories;
using Investors.Kernel.Shared.Operations.Domain.Repositories;
using Investors.Kernel.Shared.Shared.Infrastructure;
using Investors.Repository.EF.Catalogs;
using Investors.Repository.EF.Events;
using Investors.Repository.EF.Investors;
using Investors.Repository.EF.Operations;
using Investors.Repository.EF.Users;
using Investors.Repository.EF.Views.Movements.ViewMovementsOfCashBacks;
using Investors.Repository.EF.Views.Movements.ViewMovementsOfNights;

namespace Investors.Repository.EF.Shared
{
    public class RepositoryForReadManager : IRepositoryClientForReadManager, IRepositorySharedForReadManager, IRepositoryAdminForReadManager
    {
        private readonly Lazy<IUserReadRepository> _users;
        private readonly Lazy<IOperationReadRepository> _operations;
        private readonly Lazy<IInvestorReadRepository> _investors;
        private readonly Lazy<IEventReadRepository> _events;
        private readonly Lazy<ICatalogReadRepository> _catalogs;
        private readonly Lazy<IMovimentReadRepository> _movements;
        private readonly Lazy<IInvoiceReadRepository> _invoices;
        private readonly Lazy<IReceiptReadRepository> _receipts;
        private readonly Lazy<IViewMovementsOfCashbackRepository> _viewMovementsCashback;
        private readonly Lazy<IViewMovementsOfNightRepository> _viewMovementsNights;

        public RepositoryForReadManager(InvestorsDbContext repositoryContext)
        {
            _users = new Lazy<IUserReadRepository>(() => new UserRepository(repositoryContext));
            _operations = new Lazy<IOperationReadRepository>(() => new OperationRepository(repositoryContext));
            _investors = new Lazy<IInvestorReadRepository>(() => new InvestorRepository(repositoryContext));
            _events = new Lazy<IEventReadRepository>(() => new EventRepository(repositoryContext));
            _catalogs = new Lazy<ICatalogReadRepository>(() => new CatalogRepository(repositoryContext));
            _movements = new Lazy<IMovimentReadRepository>(() => new MovementRepository(repositoryContext));
            _invoices = new Lazy<IInvoiceReadRepository>(() => new InvoiceRepository(repositoryContext));
            _receipts = new Lazy<IReceiptReadRepository>(() => new ReceiptRepository(repositoryContext));
            _viewMovementsCashback = new Lazy<IViewMovementsOfCashbackRepository>(() => new ViewMovementsOfCashbackRepository(repositoryContext));
            _viewMovementsNights = new Lazy<IViewMovementsOfNightRepository>(() => new ViewMovementsOfNightRepository(repositoryContext));
        }

        public IUserReadRepository Users => _users.Value;
        public IMovimentReadRepository Movements => _movements.Value;
        public IOperationReadRepository Operations => _operations.Value;
        public IInvestorReadRepository Investors => _investors.Value;
        public IEventReadRepository Events => _events.Value;
        public ICatalogReadRepository Catalogs => _catalogs.Value;
        public IInvoiceReadRepository Invoices => _invoices.Value;
        public IReceiptReadRepository Receipts => _receipts.Value;
        public IViewMovementsOfCashbackRepository ViewMovementsOfCashback => _viewMovementsCashback.Value;
        public IViewMovementsOfNightRepository ViewMovementsOfNight => _viewMovementsNights.Value;
    }
}