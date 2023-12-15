using Mapster;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create.Models;
using PersonIdentificationDirectory.Domain.PersonAggregate.Entities;

namespace PersonIdentificationDirectory.Application.Commons.Mapping
{
    public class PersonMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PhoneNumberDto, PhoneNumber>();
        }
    }
}
