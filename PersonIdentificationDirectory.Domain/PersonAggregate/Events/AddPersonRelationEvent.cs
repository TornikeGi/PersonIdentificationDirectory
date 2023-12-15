using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.Events
{
    public sealed class AddPersonRelationEvent : BasePersonRelationEvent
    {
        public AddPersonRelationEvent(long relatedPersonId, long personId, RelationType type) : base(relatedPersonId, personId, type)
        {
        }
    }
}
