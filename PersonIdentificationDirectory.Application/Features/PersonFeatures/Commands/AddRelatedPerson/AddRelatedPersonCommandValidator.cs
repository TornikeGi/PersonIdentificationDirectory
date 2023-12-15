using FluentValidation;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandValidator : AbstractValidator<AddRelatedPersonCommand>
    {
        public AddRelatedPersonCommandValidator()
        {
            RuleFor(x => x.RelationType).IsInEnum();
            RuleFor(x => x.RelatedPersonId).NotEmpty();
            RuleFor(x => x.PersonId).NotEmpty();
        }
    }
}
