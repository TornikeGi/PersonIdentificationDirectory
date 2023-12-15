using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonIdentificationDirectory.Domain.PersonAggregate.Entities;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.TypeConfigurations
{
    public class PersonRelationTypeConfiguration : IEntityTypeConfiguration<PersonRelation>
    {
        public void Configure(EntityTypeBuilder<PersonRelation> builder)
        {
            builder
                  .Property(x => x.Type)
                  .IsRequired()
                  .HasConversion<EnumToStringConverter<RelationType>>()
                  .HasMaxLength(10);


            builder
                  .HasOne(x => x.RelatedPerson)
                  .WithMany()
                  .HasForeignKey(x => x.RelatedPersonId)
                  .OnDelete(DeleteBehavior.Restrict);


            builder
                 .HasOne(x => x.Person)
                 .WithMany(x => x.Relations)
                 .HasForeignKey(x => x.RelatedPersonId)
                 .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
