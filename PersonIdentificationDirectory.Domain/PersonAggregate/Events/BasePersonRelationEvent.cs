using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.Events
{
    public class BasePersonRelationEvent : INotification
    {
        public long RelatedPersonId { get; init; }
        public long PersonId { get; init; }
        public RelationType Type { get; init; }

        public BasePersonRelationEvent(
            long relatedPersonId,
            long personId,
            RelationType type)
        {
            RelatedPersonId = relatedPersonId;
            PersonId = personId;
            Type = type;
        }
    }
}
