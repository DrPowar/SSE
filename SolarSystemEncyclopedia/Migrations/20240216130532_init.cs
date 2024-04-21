using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystemEncyclopedia.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CosmicObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mass = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Volume = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SurfaceArea = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Radius = table.Column<long>(type: "bigint", nullable: false),
                    Density = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosmicObject", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Star",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SpectralClass = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temperature = table.Column<double>(type: "double", nullable: false),
                    Luminosity = table.Column<double>(type: "double", nullable: false),
                    HasPlanets = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Star", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Star_CosmicObject_Id",
                        column: x => x.Id,
                        principalTable: "CosmicObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    HasAtmosphere = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AtmosphericPressure = table.Column<double>(type: "double", nullable: true),
                    AverageTemperature = table.Column<double>(type: "double", nullable: true),
                    MainStarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planet_CosmicObject_Id",
                        column: x => x.Id,
                        principalTable: "CosmicObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planet_Star_MainStarId",
                        column: x => x.MainStarId,
                        principalTable: "Star",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Moon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    HasAtmosphere = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AtmosphericPressure = table.Column<double>(type: "double", nullable: true),
                    AverageTemperature = table.Column<double>(type: "double", nullable: true),
                    MainPlanetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moon_CosmicObject_Id",
                        column: x => x.Id,
                        principalTable: "CosmicObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moon_Planet_MainPlanetId",
                        column: x => x.MainPlanetId,
                        principalTable: "Planet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Moon_MainPlanetId",
                table: "Moon",
                column: "MainPlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_MainStarId",
                table: "Planet",
                column: "MainStarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moon");

            migrationBuilder.DropTable(
                name: "Planet");

            migrationBuilder.DropTable(
                name: "Star");

            migrationBuilder.DropTable(
                name: "CosmicObject");
        }
    }
}
