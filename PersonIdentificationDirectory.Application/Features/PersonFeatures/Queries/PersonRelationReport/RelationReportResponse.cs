using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.PersonRelationReport
{
    public class RelationReportResponse
    {
        public string FirstName { get; set; }
        public int Count { get; set; }
        public RelationType Type { get; set; }
        public int Id { get; set; }
    }
}
