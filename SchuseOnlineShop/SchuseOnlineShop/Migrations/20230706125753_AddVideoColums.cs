using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchuseOnlineShop.Migrations
{
    public partial class AddVideoColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Video",
                table: "Products");
        }
    }
}
