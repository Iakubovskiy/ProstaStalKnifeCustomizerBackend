using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EngravingWithLaserFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureForLaserId",
                table: "Engravings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engravings_PictureForLaserId",
                table: "Engravings",
                column: "PictureForLaserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Engravings_FileEntity_PictureForLaserId",
                table: "Engravings",
                column: "PictureForLaserId",
                principalTable: "FileEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engravings_FileEntity_PictureForLaserId",
                table: "Engravings");

            migrationBuilder.DropIndex(
                name: "IX_Engravings_PictureForLaserId",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "PictureForLaserId",
                table: "Engravings");
        }
    }
}
