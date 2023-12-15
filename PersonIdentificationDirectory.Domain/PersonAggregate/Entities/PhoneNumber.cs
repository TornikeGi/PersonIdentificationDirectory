using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;
using PersonIdentificationDirectory.Utility.Domain;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.Entities
{
    public class PhoneNumber : Entity<long>
    {
        public PhoneNumberType PhoneNumberType { get; private set; }
        public string Number { get; private set; }
        public long? PersonId { get; private set; }
        public Person? Person { get; private set; }
    }
}
