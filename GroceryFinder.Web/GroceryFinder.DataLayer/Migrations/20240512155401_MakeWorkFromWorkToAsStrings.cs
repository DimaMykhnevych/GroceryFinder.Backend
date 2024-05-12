using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryFinder.DataLayer.Migrations
{
    public partial class MakeWorkFromWorkToAsStrings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkTo",
                table: "GroceryStores",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "WorkFrom",
                table: "GroceryStores",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "WorkTo",
                table: "GroceryStores",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "WorkFrom",
                table: "GroceryStores",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
