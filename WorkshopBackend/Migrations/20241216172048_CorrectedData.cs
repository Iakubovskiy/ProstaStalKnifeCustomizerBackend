using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopBackend.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BladeCoatingColorId",
                table: "Knives",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "bladeShapeModelUrl",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "handleShapeModelUrl",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sheathModelUrl",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Knives_BladeCoatingColorId",
                table: "Knives",
                column: "BladeCoatingColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Knives_BladeCoatingColors_BladeCoatingColorId",
                table: "Knives",
                column: "BladeCoatingColorId",
                principalTable: "BladeCoatingColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knives_BladeCoatingColors_BladeCoatingColorId",
                table: "Knives");

            migrationBuilder.DropIndex(
                name: "IX_Knives_BladeCoatingColorId",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "BladeCoatingColorId",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "bladeShapeModelUrl",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleShapeModelUrl",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "sheathModelUrl",
                table: "BladeShapes");
        }
    }
}
