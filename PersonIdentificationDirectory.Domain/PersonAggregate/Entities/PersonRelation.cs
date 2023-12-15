using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;
using PersonIdentificationDirectory.Utility.Domain;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.Entities
{
    public class PersonRelation : Entity<long>
    {
        public PersonRelation() { }

        public PersonRelation(
            RelationType type,
            Person relatedPerson,
            Person person)
        {
            Type = type;
            RelatedPerson = relatedPerson;
            Person = person;
        }

        public RelationType Type { get; private set; }
        public long RelatedPersonId { get; private set; }
        public Person RelatedPerson { get; private set; }
        public long PersonId { get; private set; }
        public Person Person { get; private set; }
    }
}
