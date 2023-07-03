using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchuseOnlineShop.Migrations
{
    public partial class NewTablesWishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CategorySubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WishlistId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WishlistProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistProducts_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubCategories_ProductId",
                table: "CategorySubCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_WishlistId",
                table: "CartProducts",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistProducts_ProductId",
                table: "WishlistProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistProducts_WishlistId",
                table: "WishlistProducts",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AppUserId",
                table: "Wishlists",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Wishlists_WishlistId",
                table: "CartProducts",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySubCategories_Products_ProductId",
                table: "CategorySubCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Wishlists_WishlistId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySubCategories_Products_ProductId",
                table: "CategorySubCategories");

            migrationBuilder.DropTable(
                name: "WishlistProducts");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_CategorySubCategories_ProductId",
                table: "CategorySubCategories");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_WishlistId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CategorySubCategories");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "CartProducts");
        }
    }
}
