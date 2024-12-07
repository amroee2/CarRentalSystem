using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCarSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "CarDescription", "CarImage", "CarModel", "CarName", "IsAvailable", "RentPricePerDay" },
                values: new object[,]
                {
                    { 1, "Compact car, fuel-efficient", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2020", "Toyota Corolla", true, 50.0 },
                    { 2, "Compact car, sporty design", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2021", "Honda Civic", true, 55.0 },
                    { 3, "Reliable car, comfortable ride", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2019", "Ford Focus", true, 45.0 },
                    { 4, "Mid-size car, modern features", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2022", "Chevrolet Malibu", true, 60.0 },
                    { 5, "Affordable car, great fuel economy", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2023", "Hyundai Elantra", true, 48.0 },
                    { 6, "Mid-size sedan, comfortable seating", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2020", "Nissan Altima", true, 52.0 },
                    { 7, "Luxury sedan, powerful engine", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2021", "BMW 3 Series", true, 150.0 },
                    { 8, "Luxury car, elegant design", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2019", "Mercedes-Benz C-Class", true, 180.0 },
                    { 9, "High-performance car, premium features", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2022", "Audi A4", true, 160.0 },
                    { 10, "Compact SUV, fun to drive", "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", "2023", "Kia Soul", true, 70.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 10);
        }
    }
}
