using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Utility.Exceptions;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : IRequestHandler<AddRelatedPersonCommand>
    {
        private readonly IPersonRepository _repo;
        public AddRelatedPersonCommandHandler(IPersonRepository repo)
        {
            _repo = repo;
        }
        public async Task Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repo.FindByIdAsync(request.PersonId, cancellationToken);

            if (person == null)
                throw new NotFoundException(ErrorCodes.PersonNotFound, $"{request.PersonId}");

            var relatedPerson = await _repo.FindByIdAsync(request.RelatedPersonId, cancellationToken);

            if (relatedPerson == null)
                throw new NotFoundException(ErrorCodes.RelationPersonNotFound, $"{request.RelatedPersonId}");

            person.AddRelation(relatedPerson, request.RelationType);

            _repo.Update(person);
            await _repo.SaveAggregatesAsync(cancellationToken);
        }
    }
}
