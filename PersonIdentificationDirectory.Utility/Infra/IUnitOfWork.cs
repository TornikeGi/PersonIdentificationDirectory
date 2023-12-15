using Microsoft.EntityFrameworkCore.Storage;

namespace PersonIdentificationDirectory.Utility.Infra
{
    public interface IUnitOfWork
    {
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransaction();
        IExecutionStrategy CreateExecutionStrategy();
    }
}
