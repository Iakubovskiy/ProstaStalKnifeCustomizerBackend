using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompletedSheathAddedToDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Product",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedSheathId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedSheath_SheathColorId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedSheath_SheathId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedSheathId",
                table: "Engravings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompletedSheath_SheathColorId",
                table: "Product",
                column: "CompletedSheath_SheathColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompletedSheath_SheathId",
                table: "Product",
                column: "CompletedSheath_SheathId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompletedSheathId",
                table: "Product",
                column: "CompletedSheathId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_CompletedSheathId",
                table: "Product",
                column: "CompletedSheathId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SheathColors_CompletedSheath_SheathColorId",
                table: "Product",
                column: "CompletedSheath_SheathColorId",
                principalTable: "SheathColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Sheaths_CompletedSheath_SheathId",
                table: "Product",
                column: "CompletedSheath_SheathId",
                principalTable: "Sheaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engravings_Product_CompletedSheathId",
                table: "Engravings");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_CompletedSheathId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SheathColors_CompletedSheath_SheathColorId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Sheaths_CompletedSheath_SheathId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CompletedSheath_SheathColorId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CompletedSheath_SheathId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CompletedSheathId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Engravings_CompletedSheathId",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "CompletedSheathId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompletedSheath_SheathColorId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompletedSheath_SheathId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompletedSheathId",
                table: "Engravings");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Product",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(21)",
                oldMaxLength: 21);
        }
    }
}
