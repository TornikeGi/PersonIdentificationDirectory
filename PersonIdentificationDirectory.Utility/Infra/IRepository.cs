using PersonIdentificationDirectory.Utility.Domain;
using System.Linq.Expressions;

namespace PersonIdentificationDirectory.Utility.Infra
{
    public interface IRepository<TAggregate, TIdentifier>
        where TAggregate : AggregateRoot<TIdentifier>
    {
        Task<bool> Exists(Expression<Func<TAggregate, bool>> predicate);
        Task<TAggregate> FindByIdAsync(TIdentifier id, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveAggregatesAsync(CancellationToken cancellationToken = default);
        void Update(TAggregate aggregate);
        Task AddAsync(TAggregate entity);
        IQueryable<TAggregate> GetWithPredicate(Expression<Func<TAggregate, bool>> predicate);
    }
}
