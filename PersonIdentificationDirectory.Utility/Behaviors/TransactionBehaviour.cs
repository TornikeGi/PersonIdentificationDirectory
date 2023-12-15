using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonIdentificationDirectory.Utility.Infra;

namespace PersonIdentificationDirectory.Utility.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehaviour(IUnitOfWork unitOfWork,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(IUnitOfWork));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                var strategy = _unitOfWork.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");

                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}; \n {ex.ToString()}");

                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
