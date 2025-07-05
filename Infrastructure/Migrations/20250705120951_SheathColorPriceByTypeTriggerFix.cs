using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SheathColorPriceByTypeTriggerFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_affected_knife_prices_on_sheath_color()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE ""Product"" 
                    SET ""Id"" = ""Id""
                    WHERE ""Discriminator"" = 'Knife'
                    AND (
                        ""SheathColorId"" = COALESCE(NEW.""SheathColorId"", OLD.""SheathColorId"")
                    );
                    
                    RETURN COALESCE(NEW, OLD);
                END;
                $$ LANGUAGE plpgsql;
            ");
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS trigger_refresh_knife_prices_sheathcolorpricebytype
                    ON ""SheathColorPriceByType"";
            ");
            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_prices_sheathcolorpricebytype
                    AFTER INSERT OR UPDATE OR DELETE ON ""SheathColorPriceByType""
                    FOR EACH ROW EXECUTE FUNCTION refresh_affected_knife_prices_on_sheath_color();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
