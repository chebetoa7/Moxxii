using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moxxii.Api.Migrations
{
    /// <inheritdoc />
    public partial class LocalAddTravelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentPass = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    CommentDrive = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    StatusTrip = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    latInitial = table.Column<double>(type: "float", nullable: false),
                    LongInitial = table.Column<double>(type: "float", nullable: false),
                    latEnd = table.Column<double>(type: "float", nullable: false),
                    LongEnd = table.Column<double>(type: "float", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viaje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioViaje",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ViajesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioViaje", x => new { x.UsuarioId, x.ViajesId });
                    table.ForeignKey(
                        name: "FK_UsuarioViaje_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioViaje_Viaje_ViajesId",
                        column: x => x.ViajesId,
                        principalTable: "Viaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioViaje_ViajesId",
                table: "UsuarioViaje",
                column: "ViajesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioViaje");

            migrationBuilder.DropTable(
                name: "Viaje");
        }
    }
}
