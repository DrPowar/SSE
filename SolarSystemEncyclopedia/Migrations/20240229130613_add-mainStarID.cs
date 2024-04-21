using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystemEncyclopedia.Migrations
{
    /// <inheritdoc />
    public partial class addmainStarID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planet_Star_MainStarId",
                table: "Planet");

            migrationBuilder.RenameColumn(
                name: "MainStarId",
                table: "Planet",
                newName: "MainStarId1");

            migrationBuilder.RenameIndex(
                name: "IX_Planet_MainStarId",
                table: "Planet",
                newName: "IX_Planet_MainStarId1");

            migrationBuilder.AlterColumn<double>(
                name: "Density",
                table: "CosmicObject",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddForeignKey(
                name: "FK_Planet_Star_MainStarId1",
                table: "Planet",
                column: "MainStarId1",
                principalTable: "Star",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planet_Star_MainStarId1",
                table: "Planet");

            migrationBuilder.RenameColumn(
                name: "MainStarId1",
                table: "Planet",
                newName: "MainStarId");

            migrationBuilder.RenameIndex(
                name: "IX_Planet_MainStarId1",
                table: "Planet",
                newName: "IX_Planet_MainStarId");

            migrationBuilder.AlterColumn<double>(
                name: "Density",
                table: "CosmicObject",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Planet_Star_MainStarId",
                table: "Planet",
                column: "MainStarId",
                principalTable: "Star",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
