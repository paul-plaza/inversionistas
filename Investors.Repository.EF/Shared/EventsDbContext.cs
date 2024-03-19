using System.Diagnostics;
using Investors.Shared.Domain;
using Investors.Shared.Domain.Events;
using Investors.Shared.Infrastructure.HanfireMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Investors.Repository.EF.Shared
{
    public class EventsDbContext : IEventsDbContext
    {
        private readonly IMediator _mediator;
        public EventsDbContext(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void ManageTracked(object sender, EntityTrackedEventArgs args)
        {
            Debug.WriteLine($"Entity {args.Entry.Entity.GetType().Name} is being tracked");
        }

        public void ManageStateChange(object sender, EntityStateChangedEventArgs args)
        {
            Debug.WriteLine($"Entity {args.Entry.Entity.GetType().Name} state changed from {args.OldState} to {args.NewState}");
        }

        public void ManageSavingChanges(object sender, SavingChangesEventArgs args)
        {
            Debug.WriteLine("Saving changes");
        }

        public void ManageSavedChanges(object sender, SavedChangesEventArgs args)
        {
            if (args.EntitiesSavedCount < 0)
                return;

            var entitiesWithEvents = ((DbContext)sender).ChangeTracker
                .Entries()
                .Select(e => e.Entity as IBaseEntityEvents)
                .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity?.DomainEvents
                    .Select(e => e as IDomainTaskEvent)
                    .Where(e => e is not null)
                    .ToArray();
                // Clear events
                entity?.ClearDomainTaskEvents();

                if (events != null)
                    foreach (var domainEvent in events)
                    {
                        _mediator.Enqueue(domainEvent!.NameEvent, domainEvent);
                    }
            }
        }

        public void ManageSavedChangesFailed(object sender, SaveChangesFailedEventArgs args)
        {
            Debug.WriteLine("Saved changes failed");
        }
    }
}