using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updateNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Notification",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cupomDeDesconto",
                table: "Notification",
                type: "text",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "cupomDeDesconto",
                table: "Notification");
        }
    }
}
