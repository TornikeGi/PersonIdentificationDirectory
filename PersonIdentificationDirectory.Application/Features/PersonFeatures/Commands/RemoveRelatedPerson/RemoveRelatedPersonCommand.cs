using MediatR;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.RemoveRelatedPerson
{
    public sealed record RemoveRelatedPersonCommand(long PersonId, long RelatedPersonId) : IRequest;
}
