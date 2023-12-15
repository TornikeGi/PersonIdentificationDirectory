using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonIdentificationDirectory.Infrastructure.Persistence.DbContexts;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.DatabaseMigrate
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<PersonDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
