using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Utility.Domain;
using System.Linq.Expressions;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.Get
{
    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PagedQueryResponse<Person>>
    {
        private readonly IPersonRepository _repo;
        public GetPersonsQueryHandler(IPersonRepository repo)
        {
            _repo = repo;
        }
        public async Task<PagedQueryResponse<Person>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Person, bool>> searchTextPredicate = null;

            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                searchTextPredicate = x =>
                    x.PersonalNumber.Contains(request.SearchText) ||
                    x.FirstName.Contains(request.SearchText) ||
                    x.LastName.Contains(request.SearchText);
            }

            var result  = await _repo.GetFilteredPersons(searchTextPredicate, request.Page, request.PageSize);
            return result;
        }
    }
}
