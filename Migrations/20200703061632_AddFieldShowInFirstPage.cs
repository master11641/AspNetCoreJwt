using Microsoft.EntityFrameworkCore.Migrations;

namespace core.Migrations
{
    public partial class AddFieldShowInFirstPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowInFirstPage",
                table: "TcategoryStores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowInFirstPage",
                table: "TcategoryStores");
        }
    }
}
