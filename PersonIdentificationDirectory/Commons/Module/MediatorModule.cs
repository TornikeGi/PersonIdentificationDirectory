using Autofac;
using FluentValidation;
using MediatR;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.AddRelatedPerson;
using PersonIdentificationDirectory.Application.Features.PersonFeatures.Commands.Create;
using PersonIdentificationDirectory.Utility.Behaviors;
using System.Reflection;

namespace PersonIdentificationDirectory.API.Commons.Module
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreatePersonCommandHandler)
                    .GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.RegisterAssemblyTypes(typeof(CreatePersonCommand).GetTypeInfo().Assembly)
              .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(AddRelatedPersonCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(CreatePersonCommandValidator).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
