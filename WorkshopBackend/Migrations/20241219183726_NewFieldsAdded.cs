using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopBackend.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EngravingColorCode",
                table: "SheathColors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "handleLocationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleLocationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleLocationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleRotationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleRotationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleRotationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngravingColorCode",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "handleLocationX",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleLocationY",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleLocationZ",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleRotationX",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleRotationY",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleRotationZ",
                table: "BladeShapes");
        }
    }
}
