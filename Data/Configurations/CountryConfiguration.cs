using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CityBreaks.Web.Models;

namespace CityBreaks.Web.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");

            builder.Property(c => c.CountryCode)
                .HasMaxLength(3)
                .HasColumnName("country_code");

            builder.HasData(
                new Country { Id = 1, CountryCode = "BRA", CountryName = "Brasil" },
                new Country { Id = 2, CountryCode = "POR", CountryName = "Portugal" },
                new Country { Id = 3, CountryCode = "ESP", CountryName = "Espanha" }
            );
        }
    }
}