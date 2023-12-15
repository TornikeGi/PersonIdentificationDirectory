using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.PersonRelationReport
{
    public class GetRelationReportQueryHandler : IRequestHandler<GetRelationReportQuery, List<RelationReadModel>>
    {
        private readonly IPersonRepository _repo;
        public GetRelationReportQueryHandler(IPersonRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<RelationReadModel>> Handle(GetRelationReportQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetRelationReport();
        }
    }
}
