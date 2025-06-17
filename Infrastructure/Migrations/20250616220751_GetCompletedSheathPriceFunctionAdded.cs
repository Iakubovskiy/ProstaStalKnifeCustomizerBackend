using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetCompletedSheathPriceFunctionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                    CREATE OR REPLACE FUNCTION get_completed_sheath_total_price(product_id UUID)
                    RETURNS NUMERIC
                    IMMUTABLE
                    AS $$
                    DECLARE
                        total_price NUMERIC;
                    BEGIN
                        SELECT
                            COALESCE(s."Price", 0) +
                            COALESCE(scp."Price", 0) +
                            (
                                SELECT COUNT(DISTINCT e."Side")
                                FROM "CompletedSheathEngraving" cse
                                JOIN "Engravings" e ON e."Id" = cse."EngravingsId"
                                WHERE cse."CompletedSheathId" = p."Id"
                            ) *
                            (
                                SELECT COALESCE("Price", 0) FROM "EngravingPrices" ORDER BY "Id" LIMIT 1
                            ) +
                            (
                                SELECT COALESCE(SUM(att_p."Price"), 0)
                                FROM "AttachmentCompletedSheath" acs
                                JOIN "Product" att_p ON att_p."Id" = acs."AttachmentsId"
                                WHERE acs."CompletedSheathId" = p."Id"
                            )
                        INTO
                            total_price
                        FROM
                            "Product" p
                        LEFT JOIN "Sheaths" s ON s."Id" = p."CompletedSheath_SheathId"
                        LEFT JOIN "SheathColors" sc ON sc."Id" = p."CompletedSheath_SheathColorId"
                        LEFT JOIN "SheathColorPriceByType" scp ON scp."SheathColorId" = sc."Id" AND scp."TypeId" = s."TypeId"
                        WHERE
                            p."Id" = product_id;
                
                        RETURN COALESCE(total_price, 0);
                    END;
                    $$ LANGUAGE plpgsql;
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                    DROP FUNCTION IF EXISTS get_completed_sheath_total_price(UUID);
                """
            );
        }
    }
}
