using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitiesAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 4, 2, "Railway architecture in Belgium", "Antwerp Central Station" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
