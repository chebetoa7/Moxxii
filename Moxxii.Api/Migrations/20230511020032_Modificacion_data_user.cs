using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moxxii.Api.Migrations
{
    /// <inheritdoc />
    public partial class Modificacion_data_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tokenfirebase",
                table: "Usuarios",
                newName: "Tokenfirebase");

            migrationBuilder.AlterColumn<string>(
                name: "DataPushUpdate",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tokenfirebase",
                table: "Usuarios",
                newName: "tokenfirebase");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPushUpdate",
                table: "Usuarios",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
