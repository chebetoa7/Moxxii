using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moxxii.Api.Migrations
{
    /// <inheritdoc />
    public partial class push : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPushUpdate",
                table: "Usuarios",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "LoginNow",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "tokenfirebase",
                table: "Usuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPushUpdate",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LoginNow",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "tokenfirebase",
                table: "Usuarios");
        }
    }
}
