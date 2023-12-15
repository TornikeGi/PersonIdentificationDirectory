using PersonIdentificationDirectory.Domain.PersonAggregate.Entities;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;
using PersonIdentificationDirectory.Domain.PersonAggregate.Events;
using PersonIdentificationDirectory.Utility.Domain;

namespace PersonIdentificationDirectory.Domain.PersonAggregate
{
    public class Person : AggregateRoot<long>
    {
        #region Constructors
        public Person() { }


        public Person(
            string firstName,
            string lastName,
            string personalNumber,
            DateOnly birthDate,
            Gender gender,
            int cityId,
            IEnumerable<PhoneNumber> phoneNumbers)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;
            Gender = gender;
            CityId = cityId;
            _phoneNumbers = phoneNumbers.ToList();
        }
        #endregion

        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PersonalNumber { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public int CityId { get; private set; }
        public string? ImagePath { get; private set; }

        private List<PhoneNumber> _phoneNumbers = new();
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();

        private readonly List<PersonRelation> _relations = new();
        public IReadOnlyCollection<PersonRelation> Relations => _relations.AsReadOnly();
        #endregion

        #region DomainMethods
        public void Update(
         string? firstName,
         string? lastName,
         Gender? gender,
         DateOnly? birthDate,
         string? personalNumber,
         int? cityId)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                FirstName = firstName!;
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                LastName = lastName!;
            }

            if (gender.HasValue)
            {
                Gender = gender.Value;
            }

            if (birthDate.HasValue)
            {
                BirthDate = birthDate.Value;
            }

            if (!string.IsNullOrWhiteSpace(personalNumber))
            {
                PersonalNumber = personalNumber!;
            }

            if (cityId.HasValue)
            {
                CityId = cityId.Value;
            }

            ChangeLastUpdateDate();
        }
        private void AddPhoneNumbers(IEnumerable<PhoneNumber> phoneNumbers, bool updateAction = false)
        {
            if (updateAction)
                _phoneNumbers.Clear();

            _phoneNumbers.AddRange(phoneNumbers);
        }

        public void AddRelation(Person person, RelationType type)
        {
            _relations.Add(
                new PersonRelation(
                    type,
                    this,
                    person));

            Raise(new AddPersonRelationEvent(person.Id, this.Id, type));
        }

        public void RemoveRelation(long relatedPersonId)
        {
            var relatedPerson = _relations.First(x => x.RelatedPersonId == relatedPersonId);

            _relations.Remove(relatedPerson);

            Raise(new RemovePersonRelationEvent(relatedPerson.Id, this.Id, relatedPerson.Type));
        }

        public void Delete()
        {
            _relations.Clear();
            _phoneNumbers.Clear();

            ChangeStatusAsDeleted();
        }

        #endregion
    }

}
