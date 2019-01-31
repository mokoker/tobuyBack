using Microsoft.EntityFrameworkCore.Migrations;

namespace TB.Db.Migrations
{
    public partial class deleteMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverStatus",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderStatus",
                table: "Messages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverStatus",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderStatus",
                table: "Messages");
        }
    }
}
