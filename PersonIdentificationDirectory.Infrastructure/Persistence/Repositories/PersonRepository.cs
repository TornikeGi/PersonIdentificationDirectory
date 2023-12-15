using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Domain.PersonAggregate.Entities;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels;
using PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts;
using PersonIdentificationDirectory.Utility.Domain;
using System.Linq.Expressions;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : Repository<Person, long>, IPersonRepository
    {
        public PersonRepository(PersonDbContext dbContext, IMediator mediator)
            : base(dbContext, mediator)
        {
        }

        public async Task<PagedQueryResponse<Person>> GetFilteredPersons(Expression<Func<Person, bool>>? predicate, int page, int pageSize)
        {
            var query = GetWithPredicate(predicate);

            var totalCount = await query.CountAsync();

            var result = await query
                .Include(x => x.PhoneNumbers)
                .Include(x => x.Relations)
                    .ThenInclude(p => p.RelatedPerson)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedQueryResponse<Person>(page, pageSize, result, totalCount);
        }

        public List<RelationReadModel> GetRelationReport()
        {
            var report = (from r in _dbContext.Set<Person>()
                          join rp in _dbContext.Set<PersonRelation>() on r.Id equals rp.PersonId
                          group rp by new { PersonId = r.Id, RelationType = rp.Type } into grouped
                          select new RelationReadModel
                          {
                              LastName = grouped.First().Person.LastName,
                              FirstName = grouped.First().Person.FirstName,
                              PersonId = grouped.Key.PersonId,
                              RelationType = grouped.Key.RelationType,
                              Counter = grouped.Count(),
                          }).ToList();

            return report;
        }
    }
}
