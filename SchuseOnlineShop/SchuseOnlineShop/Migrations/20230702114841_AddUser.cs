using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchuseOnlineShop.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId1",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_AppUserId1",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "ProductComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_AppUserId1",
                table: "ProductComments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId1",
                table: "ProductComments",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
