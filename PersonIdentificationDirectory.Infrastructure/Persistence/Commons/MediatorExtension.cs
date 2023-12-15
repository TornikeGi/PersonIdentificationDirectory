using MediatR;
using PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts;
using PersonIdentificationDirectory.Utility.Domain;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.Commons
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync<T>(this IMediator mediator, PersonDbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<AggregateRoot<T>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
