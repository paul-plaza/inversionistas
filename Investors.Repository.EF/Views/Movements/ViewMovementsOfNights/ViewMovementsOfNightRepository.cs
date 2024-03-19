using Ardalis.Specification.EntityFrameworkCore;
using Investors.Administrator.Reports.Domain;
using Investors.Administrator.Reports.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Views.Movements.ViewMovementsOfNights
{
    public class ViewMovementsOfNightRepository : RepositoryBase<ViewMovementsOfNight>, IViewMovementsOfNightRepository
    {
        public ViewMovementsOfNightRepository(InvestorsDbContext dbContext) : base(dbContext)
        {

        }
    }
}