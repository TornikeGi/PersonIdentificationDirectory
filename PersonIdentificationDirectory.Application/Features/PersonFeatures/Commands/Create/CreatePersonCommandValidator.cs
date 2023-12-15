using FluentValidation;
using PersonIdentificationDirectory.Application.Commons.CommandValidationHelpers;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName)
                  .NotEmpty()
                  .MaximumLength(50)
                  .MinimumLength(2)
                  .Must(InputValidation.IsGeorgianOrEnglishLetters);

            RuleFor(x => x.LastName)
                  .NotEmpty()
                  .MaximumLength(50)
                  .MinimumLength(2)
                  .Must(InputValidation.IsGeorgianOrEnglishLetters);

            RuleFor(x => x.Gender).IsInEnum();

            RuleFor(x => x.PersonalNumber)
                  .NotEmpty()
                  .Length(11)
                  .Must(InputValidation.AllCharactersAreDigits);

            RuleFor(x => x.BirthDate)
                  .NotEmpty()
                  .Must(InputValidation.IsOlderThan18);

            RuleFor(x => x.PhoneNumbers)
                  .NotEmpty()
                  .Must(InputValidation.IsValidPhoneNumber);
        }
    }
}
