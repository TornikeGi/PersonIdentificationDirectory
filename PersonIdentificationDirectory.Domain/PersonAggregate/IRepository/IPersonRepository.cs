using PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels;
using PersonIdentificationDirectory.Utility.Domain;
using PersonIdentificationDirectory.Utility.Infra;
using System.Linq.Expressions;

namespace PersonIdentificationDirectory.Domain.PersonAggregate.IRepository
{
    public interface IPersonRepository : IRepository<Person, long>
    {
        Task<PagedQueryResponse<Person>> GetFilteredPersons(Expression<Func<Person, bool>>? predicate, int page, int pageSize);
        List<RelationReadModel> GetRelationReport();
    }
}
