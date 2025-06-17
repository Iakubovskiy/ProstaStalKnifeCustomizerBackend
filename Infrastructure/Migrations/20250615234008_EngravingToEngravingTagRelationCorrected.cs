using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EngravingToEngravingTagRelationCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngravingTags_Engravings_EngravingId",
                table: "EngravingTags");

            migrationBuilder.DropIndex(
                name: "IX_EngravingTags_EngravingId",
                table: "EngravingTags");

            migrationBuilder.DropColumn(
                name: "EngravingId",
                table: "EngravingTags");

            migrationBuilder.CreateTable(
                name: "Engraving_EngravingTag",
                columns: table => new
                {
                    EngravingId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engraving_EngravingTag", x => new { x.EngravingId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_Engraving_EngravingTag_EngravingTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "EngravingTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Engraving_EngravingTag_Engravings_EngravingId",
                        column: x => x.EngravingId,
                        principalTable: "Engravings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engraving_EngravingTag_TagsId",
                table: "Engraving_EngravingTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Engraving_EngravingTag");

            migrationBuilder.AddColumn<Guid>(
                name: "EngravingId",
                table: "EngravingTags",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngravingTags_EngravingId",
                table: "EngravingTags",
                column: "EngravingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngravingTags_Engravings_EngravingId",
                table: "EngravingTags",
                column: "EngravingId",
                principalTable: "Engravings",
                principalColumn: "Id");
        }
    }
}
