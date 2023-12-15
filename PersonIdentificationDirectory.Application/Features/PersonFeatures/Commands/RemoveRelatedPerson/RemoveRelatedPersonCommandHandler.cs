using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Utility.Exceptions;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.RemoveRelatedPerson
{
    public class RemoveRelatedPersonCommandHandler : IRequestHandler<RemoveRelatedPersonCommand>
    {
        private readonly IPersonRepository _repo;
        public RemoveRelatedPersonCommandHandler(IPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(RemoveRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repo.FindByIdAsync(request.PersonId, cancellationToken);

            if (person == null)
                throw new NotFoundException(ErrorCodes.PersonNotFound, $"{request.PersonId}");

            var relatedPerson = await _repo.FindByIdAsync(request.RelatedPersonId, cancellationToken);

            if (relatedPerson == null)
                throw new NotFoundException(ErrorCodes.RelationPersonNotFound, $"{request.RelatedPersonId}");

            person.RemoveRelation(relatedPerson.Id);

            _repo.Update(person);
            await _repo.SaveAggregatesAsync();
        }
    }
}
