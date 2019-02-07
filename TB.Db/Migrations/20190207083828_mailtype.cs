using Microsoft.EntityFrameworkCore.Migrations;

namespace TB.Db.Migrations
{
    public partial class mailtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MType",
                table: "Mails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MType",
                table: "Mails");
        }
    }
}
