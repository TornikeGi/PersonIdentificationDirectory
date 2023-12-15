using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PersonIdentificationDirectory.Utility.Infra;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts
{
    public class PersonDbContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;

        public PersonDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                if (_currentTransaction != null) await _currentTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        public async Task RollbackTransaction()
        {
            try
            {
                await _currentTransaction?.RollbackAsync()!;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonDbContext).Assembly);
        }
    }
}
