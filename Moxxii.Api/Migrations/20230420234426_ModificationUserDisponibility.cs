using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moxxii.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModificationUserDisponibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Disponibility",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "Usuarios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TypeUser",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Password",
                table: "Usuarios",
                column: "Password",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PhoneNumber",
                table: "Usuarios",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Password",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PhoneNumber",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Disponibility",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TypeUser",
                table: "Usuarios");
        }
    }
}
