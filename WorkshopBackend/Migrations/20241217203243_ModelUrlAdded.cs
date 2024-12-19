using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModelUrlAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BladeCoatings");

            migrationBuilder.AddColumn<string>(
                name: "ModelUrl",
                table: "Fastenings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialUrl",
                table: "BladeCoatings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelUrl",
                table: "Fastenings");

            migrationBuilder.DropColumn(
                name: "MaterialUrl",
                table: "BladeCoatings");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BladeCoatings",
                type: "text",
                nullable: true);
        }
    }
}
