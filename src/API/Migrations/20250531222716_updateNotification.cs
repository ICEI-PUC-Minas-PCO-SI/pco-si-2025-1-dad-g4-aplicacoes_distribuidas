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
<<<<<<< HEAD
                nullable: true,
=======
                nullable: false,
>>>>>>> 6f91623 (send email)
=======
                nullable: true,
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cupomDeDesconto",
                table: "Notification",
                type: "text",
<<<<<<< HEAD
<<<<<<< HEAD
                nullable: true,
=======
                nullable: false,
>>>>>>> 6f91623 (send email)
=======
                nullable: true,
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
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
