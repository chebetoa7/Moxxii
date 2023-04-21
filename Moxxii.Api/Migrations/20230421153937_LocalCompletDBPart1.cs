using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moxxii.Api.Migrations
{
    /// <inheritdoc />
    public partial class LocalCompletDBPart1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioViaje_Viaje_ViajesId",
                table: "UsuarioViaje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viaje",
                table: "Viaje");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Viaje");

            migrationBuilder.RenameTable(
                name: "Viaje",
                newName: "Viajes");

            migrationBuilder.AddColumn<double>(
                name: "TripPriceTotalMoxxii",
                table: "Viajes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viajes",
                table: "Viajes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Paradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViajeId = table.Column<int>(type: "int", nullable: false),
                    TripPrice = table.Column<double>(type: "float", nullable: false),
                    TripPriceMoxxii = table.Column<double>(type: "float", nullable: false),
                    CommentPass = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    CommentDrive = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    StatusTrip = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    latInitial = table.Column<double>(type: "float", nullable: false),
                    LongInitial = table.Column<double>(type: "float", nullable: false),
                    latEnd = table.Column<double>(type: "float", nullable: false),
                    LongEnd = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paradas_Viajes_ViajeId",
                        column: x => x.ViajeId,
                        principalTable: "Viajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ruta_paradaId = table.Column<int>(type: "int", nullable: true),
                    PromotionPrice = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Descrition = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promociones_Paradas_ruta_paradaId",
                        column: x => x.ruta_paradaId,
                        principalTable: "Paradas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_Id",
                table: "Viajes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paradas_Id",
                table: "Paradas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paradas_ViajeId",
                table: "Paradas",
                column: "ViajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_Id",
                table: "Promociones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_ruta_paradaId",
                table: "Promociones",
                column: "ruta_paradaId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioViaje_Viajes_ViajesId",
                table: "UsuarioViaje",
                column: "ViajesId",
                principalTable: "Viajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioViaje_Viajes_ViajesId",
                table: "UsuarioViaje");

            migrationBuilder.DropTable(
                name: "Promociones");

            migrationBuilder.DropTable(
                name: "Paradas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viajes",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_Id",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "TripPriceTotalMoxxii",
                table: "Viajes");

            migrationBuilder.RenameTable(
                name: "Viajes",
                newName: "Viaje");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Viaje",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viaje",
                table: "Viaje",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioViaje_Viaje_ViajesId",
                table: "UsuarioViaje",
                column: "ViajesId",
                principalTable: "Viaje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
