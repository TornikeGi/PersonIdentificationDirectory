using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonIdentificationDirectory.API.Commons.Converters;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.AddRelatedPerson;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create.Models;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.RemoveRelatedPerson;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.Get;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Queries.PersonRelationReport;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels;
using PersonIdentificationDirectory.Utility.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace PersonIdentificationDirectory.API.Controllers
{
    public class PersonController : BaseController
    {
        public PersonController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task CreatePerson([FromBody] CreatePersonCommand request, CancellationToken cancellationToken) =>
          await Sender.Send(request, cancellationToken);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task AddRelatedPerson([FromBody] AddRelatedPersonCommand request, CancellationToken cancellationToken) =>
          await Sender.Send(request, cancellationToken);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task RemoveRelated([FromBody] RemoveRelatedPersonCommand request, CancellationToken cancellationToken) =>
        await Sender.Send(request, cancellationToken);


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedQueryResponse<Person>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPersons([FromQuery] GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);

            if (result == null || !result.Data.Any())
                return NoContent();

            var jsonResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Converters = { new DateOnlyJsonConverter() },
            });
            return Ok(jsonResult);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RelationReadModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRelationReport([FromQuery] GetRelationReportQuery request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }
    }
}
