using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AttachmentRelationCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_CompletedSheathId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_KnifeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CompletedSheathId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_KnifeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompletedSheathId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "KnifeId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "AttachmentCompletedSheath",
                columns: table => new
                {
                    AttachmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompletedSheathId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentCompletedSheath", x => new { x.AttachmentsId, x.CompletedSheathId });
                    table.ForeignKey(
                        name: "FK_AttachmentCompletedSheath_Product_AttachmentsId",
                        column: x => x.AttachmentsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentCompletedSheath_Product_CompletedSheathId",
                        column: x => x.CompletedSheathId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentKnife",
                columns: table => new
                {
                    AttachmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    KnifeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentKnife", x => new { x.AttachmentsId, x.KnifeId });
                    table.ForeignKey(
                        name: "FK_AttachmentKnife_Product_AttachmentsId",
                        column: x => x.AttachmentsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentKnife_Product_KnifeId",
                        column: x => x.KnifeId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentCompletedSheath_CompletedSheathId",
                table: "AttachmentCompletedSheath",
                column: "CompletedSheathId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentKnife_KnifeId",
                table: "AttachmentKnife",
                column: "KnifeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentCompletedSheath");

            migrationBuilder.DropTable(
                name: "AttachmentKnife");

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedSheathId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KnifeId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompletedSheathId",
                table: "Product",
                column: "CompletedSheathId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_KnifeId",
                table: "Product",
                column: "KnifeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_CompletedSheathId",
                table: "Product",
                column: "CompletedSheathId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_KnifeId",
                table: "Product",
                column: "KnifeId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
