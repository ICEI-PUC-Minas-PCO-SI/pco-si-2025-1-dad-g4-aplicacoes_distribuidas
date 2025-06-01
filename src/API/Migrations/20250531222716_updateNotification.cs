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
<<<<<<< HEAD
                nullable: true,
=======
                nullable: false,
>>>>>>> 6f91623 (send email)
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cupomDeDesconto",
                table: "Notification",
                type: "text",
<<<<<<< HEAD
                nullable: true,
=======
                nullable: false,
>>>>>>> 6f91623 (send email)
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
