using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryFinder.DataLayer.Migrations
{
    public partial class AddedPriceUpdateSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceUpdateSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceUpdateSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceUpdateSubscription_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceUpdateSubscription_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PriceUpdateSubscription_AppUserId",
                table: "PriceUpdateSubscription",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceUpdateSubscription_ProductId",
                table: "PriceUpdateSubscription",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceUpdateSubscription");
        }
    }
}
