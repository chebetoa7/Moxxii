using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moxxii.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewTableSolicitud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "solicitudViajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPasajero = table.Column<int>(type: "int", nullable: true),
                    IdConductor = table.Column<int>(type: "int", nullable: true),
                    latInitial = table.Column<double>(type: "float", nullable: false),
                    longInitial = table.Column<double>(type: "float", nullable: false),
                    latEnd = table.Column<double>(type: "float", nullable: false),
                    longEnd = table.Column<double>(type: "float", nullable: false),
                    City = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Dictric = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitudViajes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_solicitudViajes_Id",
                table: "solicitudViajes",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "solicitudViajes");
        }
    }
}
