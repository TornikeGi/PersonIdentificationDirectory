using Autofac;
using Autofac.Extensions.DependencyInjection;
using PersonIdentificationDirectory.API.Commons;
using PersonIdentificationDirectory.API.Commons.Module;
using PersonIdentificationDirectory.API.Middlewares;
using PersonIdentificationDirectory.Infrastructure.Persistence.DatabaseMigrate;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddHealthChecks();
builder.Services
    .AddApiServices()
    .AddIUnitOfWork();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new MediatorModule());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorMiddleware>();
app.UseMiddleware<AcceptLanguageMiddleware>();

app.UseRequestLocalization();

app.MapControllers();

app.UseHealthChecks("/");

DatabaseMigrator.MigrateDatabase(app);

app.Run();

