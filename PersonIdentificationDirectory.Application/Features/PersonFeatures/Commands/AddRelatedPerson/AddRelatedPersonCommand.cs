using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.AddRelatedPerson
{
    public sealed record AddRelatedPersonCommand(long PersonId, long RelatedPersonId, RelationType RelationType) : IRequest;
}
