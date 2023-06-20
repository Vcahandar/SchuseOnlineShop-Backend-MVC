using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchuseOnlineShop.Migrations
{
    public partial class RemoveProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySubCategories_Products_ProductId",
                table: "CategorySubCategories");

            migrationBuilder.DropIndex(
                name: "IX_CategorySubCategories_ProductId",
                table: "CategorySubCategories");

            //migrationBuilder.DropColumn(
            //    name: "IsSelected",
            //    table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CategorySubCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "SubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CategorySubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubCategories_ProductId",
                table: "CategorySubCategories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySubCategories_Products_ProductId",
                table: "CategorySubCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
