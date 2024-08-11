using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Repository.TypeConfiguration
{
    public class CountryTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasOne(x => x.Region)
                .WithMany(r => r.Countries)
                .IsRequired();

            builder.HasMany(x => x.Languages)
                .WithMany(l => l.Countries)
                .UsingEntity(j => j.ToTable("CountryLanguages"));
        }
    }
}