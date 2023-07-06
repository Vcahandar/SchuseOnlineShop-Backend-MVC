using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchuseOnlineShop.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Wishlists_WishlistId",
                table: "CartProducts");

            migrationBuilder.DropTable(
                name: "ProductVideos");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_WishlistId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "CartProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishlistId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideoName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoPoster = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVideos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_WishlistId",
                table: "CartProducts",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVideos_ProductId",
                table: "ProductVideos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Wishlists_WishlistId",
                table: "CartProducts",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id");
        }
    }
}
