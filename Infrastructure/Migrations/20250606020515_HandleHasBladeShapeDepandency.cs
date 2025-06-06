using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HandleHasBladeShapeDepandency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BladeShapeTypeId",
                table: "Handles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Handles_BladeShapeTypeId",
                table: "Handles",
                column: "BladeShapeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Handles_BladeShapeTypes_BladeShapeTypeId",
                table: "Handles",
                column: "BladeShapeTypeId",
                principalTable: "BladeShapeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Handles_BladeShapeTypes_BladeShapeTypeId",
                table: "Handles");

            migrationBuilder.DropIndex(
                name: "IX_Handles_BladeShapeTypeId",
                table: "Handles");

            migrationBuilder.DropColumn(
                name: "BladeShapeTypeId",
                table: "Handles");
        }
    }
}
