using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Administrator.Users.Domain.Entities;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Domain.Entities.Notifications;
using Investors.Client.Users.Domain.Entities.Profiles;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Kernel.Shared.Catalogs.Domain.Entities.CatalogDetails;
using Investors.Kernel.Shared.Catalogs.Domain.Entities.Categories;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Kernel.Shared.Events.Domain.Entities.EventSubDetails;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;
using Investors.Repository.EF.Catalogs.Configuration.CatalogDetailEntity;
using Investors.Repository.EF.Catalogs.Configuration.CatalogEntity;
using Investors.Repository.EF.Catalogs.Configuration.CategoryEntity;
using Investors.Repository.EF.Common;
using Investors.Repository.EF.Events.Configuration.EventDetailEntity;
using Investors.Repository.EF.Events.Configuration.EventEntity;
using Investors.Repository.EF.Events.Configuration.EventSubDetailEntity;
using Investors.Repository.EF.Investors.Configuration;
using Investors.Repository.EF.Operations.Configuration.MenuEntity;
using Investors.Repository.EF.Operations.Configuration.MenuTypeEntity;
using Investors.Repository.EF.Operations.Configuration.OperationEntity;
using Investors.Repository.EF.Operations.Configuration.RestaurantEntity;
using Investors.Repository.EF.Operations.Configuration.RoomEntity;
using Investors.Repository.EF.UserAdministrators.Configuration.OptionsEntity;
using Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorEntity;
using Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorOperationEntity;
using Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorOptionEntity;
using Investors.Repository.EF.Users.Configuration.InvoiceEntity;
using Investors.Repository.EF.Users.Configuration.NotificationEntity;
using Investors.Repository.EF.Users.Configuration.ProfileEntity;
using Investors.Repository.EF.Users.Configuration.ReceiptEntity;
using Investors.Repository.EF.Users.Configuration.Transactions;
using Investors.Repository.EF.Users.Configuration.UserEntity;
using Investors.Repository.EF.Views.Movements.ViewMovementsOfCashBacks;
using Investors.Repository.EF.Views.Movements.ViewMovementsOfNights;
using Investors.Shared.Domain;
using Investors.Shared.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Investors.Repository.EF.Shared
{
    public class InvestorsDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public DbSet<User> Users { get; set; }

        public InvestorsDbContext(DbContextOptions options, IMediator mediator, IEventsDbContext? eventsDb) : base(options)
        {
            _mediator = mediator;

            if (eventsDb is not null)
            {
                SavingChanges += eventsDb.ManageSavingChanges!;
                SavedChanges += eventsDb.ManageSavedChanges!;
                SaveChangesFailed += eventsDb.ManageSavedChangesFailed!;
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
                .HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationSequencesSql(modelBuilder);


            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new InvestorConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OperationConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogDetailConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventDetailConfiguration());
            modelBuilder.ApplyConfiguration(new EventSubDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new MovementConfiguration());
            modelBuilder.ApplyConfiguration(new CashbackDetailConfiguration());
            modelBuilder.ApplyConfiguration(new NightDetailConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new InvestorOperationsConfiguration());
            modelBuilder.ApplyConfiguration(new UserAdministratorConfiguration());
            modelBuilder.ApplyConfiguration(new UserAdministratorOperationConfiguration());
            modelBuilder.ApplyConfiguration(new UserAdministratorOptionConfiguration());
            modelBuilder.ApplyConfiguration(new OptionsConfiguration());

            //vistas
            modelBuilder.ApplyConfiguration(new ViewMovementsOfCashbackConfiguration());
            modelBuilder.ApplyConfiguration(new ViewMovementsOfNightConfiguration());
        }
        private static void ConfigurationSequencesSql(ModelBuilder modelBuilder)
        {

            modelBuilder.HasSequence<int>($"{nameof(Profile)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Menu)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(MenuType)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Operation)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Restaurant)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Catalog)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(CatalogDetail)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Event)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(EventDetail)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(EventSubDetail)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Category)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Receipt)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Room)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(CashbackDetail)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(NightsDetail)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Invoice)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(InvoiceDetail)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Notification)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(UserAdministrator)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(UserAdministratorOperation)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(UserAdministratorOption)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>($"{nameof(Option)}Sequence")
                .StartsAt(1)
                .IncrementsBy(1);
        }

        // This function saves changes to the database asynchronously and also publishes any events that have occurred
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            // Save changes to the database
            var result = await base.SaveChangesAsync(cancellationToken);
            // Get entities with events
            var entitiesWithEvents = ChangeTracker
                .Entries()
                .Select(e => e.Entity as IBaseEntityEvents)
                .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
                .ToArray();
            // Publish events transactionals for each entity
            foreach (var entity in entitiesWithEvents)
            {
                var events = entity?.DomainEvents
                    .Select(e => e as IDomainTransactionEvent)
                    .Where(e => e is not null)
                    .ToArray();
                // Clear events
                entity?.ClearDomainTransactionEvents();

                if (events is not null)
                {
                    foreach (var domainEvent in events)
                    {
                        await _mediator.Publish(domainEvent!, cancellationToken).ConfigureAwait(false);
                    }
                }
            }
            return result;
        }
    }
}