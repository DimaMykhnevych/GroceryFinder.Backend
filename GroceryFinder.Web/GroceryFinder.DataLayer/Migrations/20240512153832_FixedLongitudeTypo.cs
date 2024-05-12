using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryFinder.DataLayer.Migrations
{
    public partial class FixedLongitudeTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "GroceryStores",
                newName: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "GroceryStores",
                newName: "Longtitude");
        }
    }
}
