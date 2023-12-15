using Microsoft.AspNetCore.Localization;
using PersonIdentificationDirectory.API.Commons.Converters;
using PersonIdentificationDirectory.API.Filters;
using PersonIdentificationDirectory.API.Middlewares;
using PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts;
using PersonIdentificationDirectory.Utility.Infra;
using System.Globalization;
using System.Text.Json.Serialization;

namespace PersonIdentificationDirectory.API.Commons
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services
                .AddSupportedCultures()
                .AddExceptionHandling()
                .AddAcceptLanguageMiddleware()
                .AddSwagger()
                .AddControllers()
                .ConfigureJsonConverters();

            return services;
        }

        public static IServiceCollection AddIUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>(p => p.GetRequiredService<PersonDbContext>());

            return services;
        }

        private static IMvcBuilder ConfigureJsonConverters(this IMvcBuilder builder)
        {
            return builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerLanguageFilter>();
            });

            return services;
        }

        private static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            return services.AddTransient<ErrorMiddleware>();
        }

        private static IServiceCollection AddAcceptLanguageMiddleware(this IServiceCollection services)
        {
            return services.AddTransient<AcceptLanguageMiddleware>();
        }

        private static IServiceCollection AddSupportedCultures(this IServiceCollection services)
        {
            var supportedCultures = new[]
            {
                 new CultureInfo(LanguageConstants.En),
                 new CultureInfo(LanguageConstants.Ka),
             };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(LanguageConstants.En);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return services;
        }
    }
}
