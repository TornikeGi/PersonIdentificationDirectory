using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonIdentificationDirectory.Domain.PersonAggregate.ReadModels;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.TypeConfigurations
{
    public class RelationReadModelTypeConfiguration : IEntityTypeConfiguration<RelationReadModel>
    {
        public void Configure(EntityTypeBuilder<RelationReadModel> builder)
        {
            builder.HasIndex(x => new { x.PersonId, x.RelationType }, "IX_PersonId_RelationType");
        }
    }
}
