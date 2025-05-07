using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness_Store_Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "URL" },
                values: new object[,]
                {
                    { 3, 1, "High-quality compound for strength and muscle growth.", "Creatine Monohydrate", 29.99m, "https://www.silabg.com/uf/product/148_pm_micronized-creatine-powder-optimum-nutrition-466x801.jpg" },
                    { 4, 3, "Supports the knee during squats and variations exercises.", "Knee Sleeves", 29.99m, "https://warmbody-coldmind.com/cdn/shop/files/Thegirlisputtingonkneesleeves.jpg?v=1704913120" },
                    { 5, 2, "Allows for easy movement and ventilation.", "Shorts", 19.50m, "https://m.media-amazon.com/images/I/61OGyjgFBEL._AC_UL480_FMwebp_QL65_.jpg" },
                    { 6, 2, "The perfect footwear for running and jogging.", "Sneekers", 129.99m, "https://m.media-amazon.com/images/I/81iJLmjmuLL._AC_UL480_FMwebp_QL65_.jpg" },
                    { 7, 3, "Accessable gym equipment for working out anywhere.", "Resist band", 14.00m, "https://m.media-amazon.com/images/I/81LLONLLdSL._AC_UL480_FMwebp_QL65_.jpg" },
                    { 8, 3, "Pull-Up bar for versatile home workouts.", "Pull up bar.", 19.19m, "https://m.media-amazon.com/images/I/71yILyDij-L._AC_UL480_FMwebp_QL65_.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
