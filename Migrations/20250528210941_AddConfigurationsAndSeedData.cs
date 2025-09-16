using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityBreaks.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerNight",
                table: "Properties",
                newName: "price_per_night");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Properties",
                newName: "property_name");

            migrationBuilder.RenameColumn(
                name: "CountryName",
                table: "Countries",
                newName: "country_name");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Countries",
                newName: "country_code");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cities",
                newName: "city_name");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "country_code", "country_name" },
                values: new object[,]
                {
                    { 1, "BRA", "Brasil" },
                    { 2, "POR", "Portugal" },
                    { 3, "ESP", "Espanha" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "city_name" },
                values: new object[,]
                {
                    { 1, 1, "Rio de Janeiro" },
                    { 2, 1, "São Paulo" },
                    { 3, 2, "Lisboa" },
                    { 4, 2, "Porto" },
                    { 5, 3, "Madrid" },
                    { 6, 3, "Barcelona" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "CityId", "DeletedAt", "property_name", "price_per_night" },
                values: new object[,]
                {
                    { 1, 1, null, "Hotel Copacabana Palace", 450.00m },
                    { 2, 1, null, "Pousada Ipanema", 180.00m },
                    { 3, 2, null, "Hotel Unique", 380.00m },
                    { 4, 2, null, "Apartamento Vila Madalena", 120.00m },
                    { 5, 3, null, "Pousada do Torel", 95.00m },
                    { 6, 4, null, "Hotel Infante Sagres", 140.00m },
                    { 7, 5, null, "Hotel Ritz Madrid", 320.00m },
                    { 8, 6, null, "Casa Bonay", 85.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "property_name",
                table: "Properties",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "price_per_night",
                table: "Properties",
                newName: "PricePerNight");

            migrationBuilder.RenameColumn(
                name: "country_name",
                table: "Countries",
                newName: "CountryName");

            migrationBuilder.RenameColumn(
                name: "country_code",
                table: "Countries",
                newName: "CountryCode");

            migrationBuilder.RenameColumn(
                name: "city_name",
                table: "Cities",
                newName: "Name");
        }
    }
}
