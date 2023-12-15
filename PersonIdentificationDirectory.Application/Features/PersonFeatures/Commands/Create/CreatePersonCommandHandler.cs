using Mapster;
using MediatR;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create.Models;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Domain.PersonAggregate.Entities;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Utility.Exceptions;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponse>
    {
        private readonly IPersonRepository _repository;
        public CreatePersonCommandHandler(IPersonRepository repository)
        {
            _repository = repository;
        }
        public async Task<CreatePersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            bool personExists = await _repository.Exists(x => x.PersonalNumber == request.PersonalNumber);

            if (personExists)
                throw new NotFoundException(ErrorCodes.PersonNotFound, $"{request.PersonalNumber}");

            if (!City.Exists(request.CityId))
                throw new NotFoundException(ErrorCodes.CityNotFound, $"{request.CityId}");

            var phoneNumbers = request.PhoneNumbers.Adapt<IEnumerable<PhoneNumber>>();

            Person person = new(request.FirstName, request.LastName, request.PersonalNumber,
                request.BirthDate, request.Gender, request.CityId, phoneNumbers);

            await _repository.AddAsync(person);
            await _repository.SaveAggregatesAsync(cancellationToken);

            return new CreatePersonResponse(person.Id);
        }
    }
}
