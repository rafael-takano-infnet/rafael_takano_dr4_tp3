using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CityBreaks.Web.Models;

namespace CityBreaks.Web.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .HasColumnName("property_name");

            builder.Property(p => p.PricePerNight)
                .HasPrecision(10, 2)
                .HasColumnName("price_per_night");

            builder.HasData(
                new Property { Id = 1, Name = "Hotel Copacabana Palace", PricePerNight = 450.00m, CityId = 1 },
                new Property { Id = 2, Name = "Pousada Ipanema", PricePerNight = 180.00m, CityId = 1 },
                new Property { Id = 3, Name = "Hotel Unique", PricePerNight = 380.00m, CityId = 2 },
                new Property { Id = 4, Name = "Apartamento Vila Madalena", PricePerNight = 120.00m, CityId = 2 },
                new Property { Id = 5, Name = "Pousada do Torel", PricePerNight = 95.00m, CityId = 3 },
                new Property { Id = 6, Name = "Hotel Infante Sagres", PricePerNight = 140.00m, CityId = 4 },
                new Property { Id = 7, Name = "Hotel Ritz Madrid", PricePerNight = 320.00m, CityId = 5 },
                new Property { Id = 8, Name = "Casa Bonay", PricePerNight = 85.00m, CityId = 6 }
            );
        }
    }
}