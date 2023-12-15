using Autofac;
using Microsoft.EntityFrameworkCore;
using PersonIdentificationDirectory.Domain.PersonAggregate.IRepository;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts;
using PersonIdentificationDirectory.Infrastructure.Persistence.Repositories;
using PersonIdentificationDirectory.Utility;
using PersonIdentificationDirectory.Utility.Infra;

namespace PersonIdentificationDirectory.API.Commons.Module
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<,>))
                   .As(typeof(IRepository<,>));

            builder.RegisterType<PersonRepository>() 
                      .As<IPersonRepository>()
                      .InstancePerLifetimeScope();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var connectionString = configuration.GetConnectionString(nameof(PersonDbContext));

                var dbContextOptionsBuilder = new DbContextOptionsBuilder<PersonDbContext>()
                    .UseSqlServer(connectionString);

                return new PersonDbContext(dbContextOptionsBuilder.Options);
            }).As<PersonDbContext>().InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddHttpClient();
                return serviceCollection.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
            }).As<IHttpClientFactory>().SingleInstance();
        }
    }
}
