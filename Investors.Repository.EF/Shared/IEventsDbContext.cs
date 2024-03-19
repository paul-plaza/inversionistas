using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Investors.Repository.EF.Shared
{
    public interface IEventsDbContext
    {
        void ManageTracked(object sender, EntityTrackedEventArgs args);
        void ManageStateChange(object sender, EntityStateChangedEventArgs args);
        void ManageSavingChanges(object sender, SavingChangesEventArgs args);
        void ManageSavedChanges(object sender, SavedChangesEventArgs args);
        void ManageSavedChangesFailed(object sender, SaveChangesFailedEventArgs args);
    }
}