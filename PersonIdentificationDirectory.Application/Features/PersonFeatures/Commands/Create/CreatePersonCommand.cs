using MediatR;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create.Models;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create
{
    public sealed record CreatePersonCommand(
      string FirstName,
      string LastName,
      Gender Gender,
      string PersonalNumber,
      DateOnly BirthDate,
      int CityId,
      IEnumerable<PhoneNumberDto> PhoneNumbers) : IRequest<CreatePersonResponse>;
}
