using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetKnifePriceFunctionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                    CREATE OR REPLACE FUNCTION get_knife_total_price(product_id UUID)
                    RETURNS NUMERIC
                    IMMUTABLE
                    AS $$
                    DECLARE
                        total_price NUMERIC;
                    BEGIN
                        SELECT
                            COALESCE(bs."Price", 0) +
                            COALESCE(bc."Price", 0) +
                            COALESCE(h."Price", 0) +
                            COALESCE(s."Price", 0) +
                            COALESCE(scp."Price", 0) +
                            (
                                SELECT COUNT(DISTINCT e."Side")
                                FROM "EngravingKnife" ek
                                JOIN "Engravings" e ON e."Id" = ek."EngravingsId"
                                WHERE ek."KnifeId" = p."Id"
                            ) *
                            (
                                SELECT COALESCE("Price", 0) FROM "EngravingPrices" ORDER BY "Id" LIMIT 1
                            ) +
                            (
                                SELECT COALESCE(SUM(att_p."Price"), 0)
                                FROM "AttachmentKnife" ak
                                JOIN "Product" att_p ON att_p."Id" = ak."AttachmentsId"
                                WHERE ak."KnifeId" = p."Id"
                            )
                        INTO
                            total_price
                        FROM
                            "Product" p
                        LEFT JOIN "BladeShapes" bs ON bs."Id" = p."BladeId"
                        LEFT JOIN "BladeCoatingColors" bc ON bc."Id" = p."ColorId"
                        LEFT JOIN "Handles" h ON h."Id" = p."HandleId"
                        LEFT JOIN "Sheaths" s ON s."Id" = p."SheathId"
                        LEFT JOIN "SheathColors" sc ON sc."Id" = p."SheathColorId"
                        LEFT JOIN "SheathColorPriceByType" scp ON scp."SheathColorId" = sc."Id" AND scp."TypeId" = bs."TypeId"
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
                    DROP FUNCTION IF EXISTS get_knife_total_price(UUID);
                """
            );
        }
    }
}
