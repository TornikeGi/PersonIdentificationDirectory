using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.Events
{
    public class RemovePersonRelationEvent : BasePersonRelationEvent
    {
        public RemovePersonRelationEvent(long relatedPersonId, long personId, RelationType type) : base(relatedPersonId, personId, type)
        {
        }
    }
}
