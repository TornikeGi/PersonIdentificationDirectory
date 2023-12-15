using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create.Models;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;
using System.Text.RegularExpressions;

namespace PersonIdentificationDirectory.Application.Commons.CommandValidationHelpers
{
    public static class InputValidation
    {
        public static bool CheckIfAllCharsAreEnglish(string input)
        {
            Regex regex = new("^[a-zA-Z]+$");
            return regex.IsMatch(input);
        }

        public static bool CheckIfAllCharsAreGeorgian(string input)
        {
            Regex regex = new("^[ა-ჰ]+$");
            return regex.IsMatch(input);
        }

        public static bool IsGeorgianOrEnglishLetters(string text)
        {
            bool isEnglish = CheckIfAllCharsAreEnglish(text);
            bool isGeorgian = CheckIfAllCharsAreGeorgian(text);

            return (isEnglish && !isGeorgian) || (!isEnglish && isGeorgian);
        }

        public static bool AllCharactersAreDigits(string text)
        {
            return text.All(c => char.IsDigit(c));
        }

        public static bool IsOlderThan18(DateOnly birthDate)
        {
            var currentDate = DateTime.UtcNow;

            int age = currentDate.Year - birthDate.Year;

            if (currentDate.DayOfYear < birthDate.DayOfYear)
                age--;

            return age > 18;
        }

        public static bool IsValidPhoneNumber(IEnumerable<PhoneNumberDto> phoneNumbers)
        {
            return phoneNumbers.All(dto =>
                  Enum.IsDefined(typeof(PhoneNumberType), dto.Type) && 
                  !string.IsNullOrEmpty(dto.Number) && 
                  dto.Number.Length >= 4 && dto.Number.Length <= 18); 
        }
    }
}
