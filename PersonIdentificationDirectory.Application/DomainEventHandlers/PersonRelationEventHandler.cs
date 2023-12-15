using PersonIdentificationDirectory.Application.Commons;
using PersonIdentificationDirectory.Domain.PersonAggregate.Events;

namespace PersonIdentificationDirectory.Application.DomainEventHandlers
{
    public class PersonRelationEventHandler :
         IDomainEventHandler<AddPersonRelationEvent>,
         IDomainEventHandler<RemovePersonRelationEvent>
    {
        public async Task Handle(AddPersonRelationEvent @event, CancellationToken cancellationToken)
        {

        }

        public async Task Handle(RemovePersonRelationEvent @event, CancellationToken cancellationToken)
        {

        }
    }
}
