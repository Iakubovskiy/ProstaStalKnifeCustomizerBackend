using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EngravingToCompletedSheathRelationCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engravings_Product_CompletedSheathId",
                table: "Engravings");

            migrationBuilder.DropIndex(
                name: "IX_Engravings_CompletedSheathId",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "CompletedSheathId",
                table: "Engravings");

            migrationBuilder.CreateTable(
                name: "CompletedSheathEngraving",
                columns: table => new
                {
                    CompletedSheathId = table.Column<Guid>(type: "uuid", nullable: false),
                    EngravingsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedSheathEngraving", x => new { x.CompletedSheathId, x.EngravingsId });
                    table.ForeignKey(
                        name: "FK_CompletedSheathEngraving_Engravings_EngravingsId",
                        column: x => x.EngravingsId,
                        principalTable: "Engravings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedSheathEngraving_Product_CompletedSheathId",
                        column: x => x.CompletedSheathId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedSheathEngraving_EngravingsId",
                table: "CompletedSheathEngraving",
                column: "EngravingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedSheathEngraving");

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedSheathId",
                table: "Engravings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engravings_CompletedSheathId",
                table: "Engravings",
                column: "CompletedSheathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Engravings_Product_CompletedSheathId",
                table: "Engravings",
                column: "CompletedSheathId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
