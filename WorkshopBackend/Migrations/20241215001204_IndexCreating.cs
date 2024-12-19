using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopBackend.Migrations
{
    /// <inheritdoc />
    public partial class IndexCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BladeCoatingColors_BladeCoating_BladeCoatingId",
                table: "BladeCoatingColors");

            migrationBuilder.DropForeignKey(
                name: "FK_Knives_BladeCoating_BladeCoatingId",
                table: "Knives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BladeCoating",
                table: "BladeCoating");

            migrationBuilder.RenameTable(
                name: "BladeCoating",
                newName: "BladeCoatings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BladeCoatings",
                table: "BladeCoatings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BladeCoatingColors_ColorCode",
                table: "BladeCoatingColors",
                column: "ColorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_BladeCoatingColors_BladeCoatings_BladeCoatingId",
                table: "BladeCoatingColors",
                column: "BladeCoatingId",
                principalTable: "BladeCoatings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Knives_BladeCoatings_BladeCoatingId",
                table: "Knives",
                column: "BladeCoatingId",
                principalTable: "BladeCoatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BladeCoatingColors_BladeCoatings_BladeCoatingId",
                table: "BladeCoatingColors");

            migrationBuilder.DropForeignKey(
                name: "FK_Knives_BladeCoatings_BladeCoatingId",
                table: "Knives");

            migrationBuilder.DropIndex(
                name: "IX_BladeCoatingColors_ColorCode",
                table: "BladeCoatingColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BladeCoatings",
                table: "BladeCoatings");

            migrationBuilder.RenameTable(
                name: "BladeCoatings",
                newName: "BladeCoating");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BladeCoating",
                table: "BladeCoating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BladeCoatingColors_BladeCoating_BladeCoatingId",
                table: "BladeCoatingColors",
                column: "BladeCoatingId",
                principalTable: "BladeCoating",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Knives_BladeCoating_BladeCoatingId",
                table: "Knives",
                column: "BladeCoatingId",
                principalTable: "BladeCoating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
