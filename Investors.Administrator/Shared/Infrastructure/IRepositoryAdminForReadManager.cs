using Investors.Administrator.Reports.Repositories;

namespace Investors.Administrator.Shared.Infrastructure
{
    public interface IRepositoryAdminForReadManager
    {
        IViewMovementsOfCashbackRepository ViewMovementsOfCashback { get; }

        IViewMovementsOfNightRepository ViewMovementsOfNight { get; }
    }
}