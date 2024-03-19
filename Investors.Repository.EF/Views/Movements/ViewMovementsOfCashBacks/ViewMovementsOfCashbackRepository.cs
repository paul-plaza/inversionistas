using Ardalis.Specification.EntityFrameworkCore;
using Investors.Administrator.Reports.Domain;
using Investors.Administrator.Reports.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Views.Movements.ViewMovementsOfCashBacks
{
    public class ViewMovementsOfCashbackRepository : RepositoryBase<ViewMovementsOfCashback>, IViewMovementsOfCashbackRepository
    {
        public ViewMovementsOfCashbackRepository(InvestorsDbContext dbContext) : base(dbContext)
        {

        }
    }
}