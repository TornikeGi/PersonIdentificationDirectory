using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.Configs
{
    internal class DatabaseOptionsSetup : IConfigureOptions<DatabaseConfig>
    {
        private const string ConfigurationSectionName = "DatabaseOptions";

        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseConfig options)
        {
            var connectionString = _configuration.GetConnectionString("Database");

            options.ConnectionString = connectionString;

            _configuration
                .GetSection(ConfigurationSectionName)
                .Bind(options);
        }
    }
}
