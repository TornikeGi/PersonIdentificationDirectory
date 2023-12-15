using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonIdentificationDirectory.Infrastructure.Persistence.Commons;
using PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts;
using PersonIdentificationDirectory.Utility.Domain;
using PersonIdentificationDirectory.Utility.Infra;
using System.Linq.Expressions;
using System.Threading;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.Repositories
{
    public class Repository<TAggregate, TIdentifier> : IRepository<TAggregate, TIdentifier>
          where TAggregate : AggregateRoot<TIdentifier>
          where TIdentifier : IEquatable<TIdentifier>
    {
        protected readonly PersonDbContext _dbContext;
        protected readonly IMediator _mediator;

        public Repository(PersonDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<TAggregate> FindByIdAsync(TIdentifier id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TAggregate>()
                                   .SingleAsync(x => x.Id.Equals(id));
        }

        public async Task<bool> Exists(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _dbContext.Set<TAggregate>().AnyAsync(predicate);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in _dbContext.ChangeTracker.Entries<AggregateRoot<TIdentifier>>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreateDate = DateTime.UtcNow;

                    item.Entity.LastChangeDate = DateTime.UtcNow;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.LastChangeDate = DateTime.UtcNow;
                }
            }

            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task<bool> SaveAggregatesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync<TIdentifier>(_dbContext);

            foreach (var item in _dbContext.ChangeTracker.Entries<AggregateRoot<TIdentifier>>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreateDate = DateTime.UtcNow;

                    item.Entity.LastChangeDate = DateTime.UtcNow;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.LastChangeDate = DateTime.UtcNow;
                }
            }

            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public void Update(TAggregate aggregate)
        {
            _dbContext.Set<TAggregate>().Update(aggregate);
        }

        public async Task AddAsync(TAggregate entity)
        {
            await _dbContext.Set<TAggregate>().AddAsync(entity);
        }

        public IQueryable<TAggregate> GetWithPredicate(Expression<Func<TAggregate, bool>>? predicate = null)
        {
            IQueryable<TAggregate> query = _dbContext.Set<TAggregate>();

            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }
    }
}
