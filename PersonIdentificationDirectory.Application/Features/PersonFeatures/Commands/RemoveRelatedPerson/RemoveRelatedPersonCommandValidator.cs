using FluentValidation;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.RemoveRelatedPerson
{
    public class RemoveRelatedPersonCommandValidator : AbstractValidator<RemoveRelatedPersonCommand>
    {
        public RemoveRelatedPersonCommandValidator()
        {
            RuleFor(x => x.RelatedPersonId).NotEmpty();
            RuleFor(x => x.PersonId).NotEmpty();
        }
    }
}
