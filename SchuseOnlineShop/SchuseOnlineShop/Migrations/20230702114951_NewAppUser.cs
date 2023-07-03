using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchuseOnlineShop.Migrations
{
    public partial class NewAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_AppUserId",
                table: "ProductComments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId",
                table: "ProductComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_AppUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ProductComments");
        }
    }
}
