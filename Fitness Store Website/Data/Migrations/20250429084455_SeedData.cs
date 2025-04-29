using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness_Store_Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "URL" },
                values: new object[,]
                {
                    { 1, "High-quality whey protein for muscle growth.", "Whey Protein", 79.99m, "https://www.silabg.com/uf/product/176_pm_2270.jpg" },
                    { 2, "Breathable sports t-shirt.", "Fitness T-Shirt", 29.99m, "https://resources.fitshop.com/bilder/cardiostrong/textilien/tsm/cst-shirt-herren_1600.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
