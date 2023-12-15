using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonIdentificationDirectory.Domain.PersonAggregate;
using PersonIdentificationDirectory.Domain.PersonAggregate.Enums;

namespace PersonIdentificationDirectory.Infrastructure.Persistence.TypeConfigurations
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
             .Property(x => x.Id)
             .ValueGeneratedOnAdd();

            builder
             .Property(x => x.FirstName)
             .IsRequired()
             .HasMaxLength(50);

            builder
             .Property(x => x.LastName)
             .IsRequired()
             .HasMaxLength(50);

            builder
             .Property(x => x.Gender)
             .IsRequired()
             .HasConversion<EnumToStringConverter<Gender>>()
             .HasMaxLength(8);

            builder
             .HasIndex(x => x.PersonalNumber)
             .IsUnique();

            builder
             .Property(x => x.PersonalNumber)
             .IsUnicode(false)
             .IsRequired()
             .HasMaxLength(11);

            builder
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder
             .Property(x => x.BirthDate)
             .IsRequired()
             .HasConversion(
                 v => v.ToDateTime(TimeOnly.MinValue),
                 v => DateOnly.FromDateTime(v))
             .HasColumnType("date");

            builder
             .HasMany(x => x.PhoneNumbers)
             .WithOne(x => x.Person)
             .HasForeignKey(x => x.PersonId)
             .OnDelete(DeleteBehavior.Cascade);

            builder
             .Navigation(x => x.PhoneNumbers)
             .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.Relations)
                   .WithOne(r => r.Person)
                   .HasForeignKey(r => r.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder
              .Navigation(x => x.Relations)
              .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
              .HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
