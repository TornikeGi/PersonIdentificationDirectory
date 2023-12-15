using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels
{
    public class RelationReadModel
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public int Counter { get; set; }
        public RelationType RelationType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RelationReadModel() { }

        public RelationReadModel(
            long personId,
            RelationType relationType,
            string firstName,
            string lastName)
        {
            PersonId = personId;
            RelationType = relationType;
            FirstName = firstName;
            LastName = lastName;
        }

        public void Increase()
        {
            Counter++;
        }

        public void Decrease()
        {
            Counter--;
        }
    }
}
