using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KnivesAndCompletedSheathTotalPriceInUahComputedColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CompletedSheath_TotalPriceInUah",
                table: "Product",
                type: "double precision",
                nullable: true,
                computedColumnSql: "public.get_completed_sheath_total_price(\"Id\")",
                stored: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPriceInUah",
                table: "Product",
                type: "double precision",
                nullable: true,
                computedColumnSql: "public.get_knife_total_price(\"Id\")",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedSheath_TotalPriceInUah",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TotalPriceInUah",
                table: "Product");
        }
    }
}
