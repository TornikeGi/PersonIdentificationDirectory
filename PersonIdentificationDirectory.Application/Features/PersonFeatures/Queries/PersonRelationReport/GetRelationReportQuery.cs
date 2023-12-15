using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.PersonRelationReport
{
    public sealed record GetRelationReportQuery() : IRequest<List<RelationReadModel>>;
}
