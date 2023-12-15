using MediatR;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Utility.Domain;

namespace PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.Get
{
    public sealed record GetPersonsQuery(
        string? SearchText,
        int PageSize,
        int Page) : IRequest<PagedQueryResponse<Person>>;
}
