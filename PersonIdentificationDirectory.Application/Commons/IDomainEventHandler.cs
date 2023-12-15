using MediatR;

namespace PersonIdentificationDirectory.Application.Commons
{
    public interface IDomainEventHandler<T> : INotificationHandler<T>
             where T : INotification
    {
        new Task Handle(T @event, CancellationToken cancellationToken);
    }
}
