using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create.Models
{
    public sealed record PhoneNumberDto(PhoneNumberType Type, string Number);
}
