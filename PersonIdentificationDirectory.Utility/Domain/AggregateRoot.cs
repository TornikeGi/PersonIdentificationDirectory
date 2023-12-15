using MediatR;

namespace PersonIdentificationDirectory.Utility.Domain
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        public DateTimeOffset? LastChangeDate { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset? DeleteAt { get; protected set; }

        public bool IsDeleted { get; protected set; }


        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void Raise(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public void ChangeLastUpdateDate()
        {
            LastChangeDate = DateTimeOffset.Now;
        }

        public void ChangeStatusAsDeleted()
        {
            IsDeleted = true;
            DeleteAt = DateTimeOffset.Now;
        }

    }
}
