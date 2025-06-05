using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BladeShpaeOptimized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "engravingLocationX",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "engravingLocationY",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "engravingLocationZ",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "engravingRotationX",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "engravingRotationY",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "engravingRotationZ",
                table: "BladeShapes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "engravingLocationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "engravingLocationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "engravingLocationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "engravingRotationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "engravingRotationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "engravingRotationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);
        }
    }
}
