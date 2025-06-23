using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KnifePriceTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_affected_knife_prices()
                RETURNS TRIGGER AS $$
                BEGIN
                    -- Оновити тільки ті ножі, які використовують змінений компонент
                    UPDATE ""Product"" 
                    SET ""Id"" = ""Id""
                    WHERE ""Discriminator"" = 'Knife'
                    AND (
                        ""BladeId"" = COALESCE(NEW.""Id"", OLD.""Id"") OR
                        ""ColorId"" = COALESCE(NEW.""Id"", OLD.""Id"") OR
                        ""HandleId"" = COALESCE(NEW.""Id"", OLD.""Id"") OR
                        ""SheathId"" = COALESCE(NEW.""Id"", OLD.""Id"") OR
                        ""SheathColorId"" = COALESCE(NEW.""Id"", OLD.""Id"")
                    );
                    
                    RETURN COALESCE(NEW, OLD);
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_knife_price_by_relation()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE ""Product"" 
                    SET ""Id"" = ""Id""
                    WHERE ""Id"" = COALESCE(NEW.""KnifeId"", OLD.""KnifeId"")
                    AND ""Discriminator"" = 'Knife';
                    
                    RETURN COALESCE(NEW, OLD);
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_all_knife_prices_on_engraving_price_change()
                RETURNS TRIGGER AS $$
                BEGIN
                    -- При зміні ціни гравіровки оновити всі ножі які мають гравіровки
                    UPDATE ""Product"" 
                    SET ""Id"" = ""Id""
                    WHERE ""Discriminator"" = 'Knife'
                    AND ""Id"" IN (
                        SELECT DISTINCT ek.""KnifeId""
                        FROM ""EngravingKnife"" ek
                    );
                    
                    RETURN COALESCE(NEW, OLD);
                END;
                $$ LANGUAGE plpgsql;
            ");

            var componentTables = new[]
            {
                "BladeShapes",
                "BladeCoatingColors", 
                "Handles",
                "Sheaths",
                "SheathColors"
            };

            foreach (var table in componentTables)
            {
                var triggerName = $"trigger_refresh_knife_prices_{table.ToLower()}";
                
                migrationBuilder.Sql($@"
                    CREATE TRIGGER {triggerName}
                        AFTER INSERT OR UPDATE OR DELETE ON ""{table}""
                        FOR EACH ROW EXECUTE FUNCTION refresh_affected_knife_prices();
                ");
            }

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_prices_sheathcolorpricebytype
                    AFTER INSERT OR UPDATE OR DELETE ON ""SheathColorPriceByType""
                    FOR EACH ROW EXECUTE FUNCTION refresh_affected_knife_prices();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_price_engraving
                    AFTER INSERT OR UPDATE OR DELETE ON ""EngravingKnife""
                    FOR EACH ROW EXECUTE FUNCTION refresh_knife_price_by_relation();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_price_attachment
                    AFTER INSERT OR UPDATE OR DELETE ON ""AttachmentKnife""
                    FOR EACH ROW EXECUTE FUNCTION refresh_knife_price_by_relation();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_prices_engraving_prices
                    AFTER INSERT OR UPDATE OR DELETE ON ""EngravingPrices""
                    FOR EACH ROW EXECUTE FUNCTION refresh_all_knife_prices_on_engraving_price_change();
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_knife_prices_on_engraving_change()
                RETURNS TRIGGER AS $$
                BEGIN
                    -- Оновити ножі які пов'язані з цим гравіруванням
                    UPDATE ""Product"" 
                    SET ""Id"" = ""Id""
                    WHERE ""Discriminator"" = 'Knife'
                    AND ""Id"" IN (
                        SELECT ek.""KnifeId""
                        FROM ""EngravingKnife"" ek
                        WHERE ek.""EngravingsId"" = COALESCE(NEW.""Id"", OLD.""Id"")
                    );
                    
                    RETURN COALESCE(NEW, OLD);
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_prices_engravings
                    AFTER INSERT OR UPDATE OR DELETE ON ""Engravings""
                    FOR EACH ROW EXECUTE FUNCTION refresh_knife_prices_on_engraving_change();
            ");

            // 9. Тригер для самої таблиці Product при зміні Knife (якщо міняються FK)
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_knife_price_on_knife_update()
                RETURNS TRIGGER AS $$
                BEGIN
                    -- Якщо змінився сам ніж (його компоненти), перерахувати ціну
                    IF NEW.""Discriminator"" = 'Knife' AND (
                        OLD.""BladeId"" IS DISTINCT FROM NEW.""BladeId"" OR
                        OLD.""ColorId"" IS DISTINCT FROM NEW.""ColorId"" OR
                        OLD.""HandleId"" IS DISTINCT FROM NEW.""HandleId"" OR
                        OLD.""SheathId"" IS DISTINCT FROM NEW.""SheathId"" OR
                        OLD.""SheathColorId"" IS DISTINCT FROM NEW.""SheathColorId""
                    ) THEN
                        NEW.""TotalPriceInUah"" := public.get_knife_total_price(NEW.""Id"");
                    END IF;
                    
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_refresh_knife_price_on_update
                    BEFORE UPDATE ON ""Product""
                    FOR EACH ROW EXECUTE FUNCTION refresh_knife_price_on_knife_update();
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION set_knife_price_on_insert()
                RETURNS TRIGGER AS $$
                BEGIN
                    IF NEW.""Discriminator"" = 'Knife' THEN
                        NEW.""TotalPriceInUah"" := public.get_knife_total_price(NEW.""Id"");
                    END IF;
                    
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trigger_set_knife_price_on_insert
                    BEFORE INSERT ON ""Product""
                    FOR EACH ROW EXECUTE FUNCTION set_knife_price_on_insert();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var triggers = new[]
            {
                "trigger_refresh_knife_prices_bladeshapes",
                "trigger_refresh_knife_prices_bladecoatingcolors",
                "trigger_refresh_knife_prices_handles", 
                "trigger_refresh_knife_prices_sheaths",
                "trigger_refresh_knife_prices_sheathcolors",
                "trigger_refresh_knife_prices_sheathcolorpricebytype",
                "trigger_refresh_knife_price_engraving",
                "trigger_refresh_knife_price_attachment",
                "trigger_refresh_knife_prices_engraving_prices",
                "trigger_refresh_knife_prices_engravings",
                "trigger_refresh_knife_price_on_update",
                "trigger_set_knife_price_on_insert"
            };

            foreach (var trigger in triggers)
            {
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""Product"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""BladeShapes"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""BladeCoatingColors"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""Handles"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""Sheaths"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""SheathColors"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""SheathColorPriceByType"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""EngravingKnife"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""AttachmentKnife"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""EngravingPrices"";");
                migrationBuilder.Sql($@"DROP TRIGGER IF EXISTS {trigger} ON ""Engravings"";");
            }

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS refresh_affected_knife_prices();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS refresh_knife_price_by_relation();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS refresh_all_knife_prices_on_engraving_price_change();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS refresh_knife_prices_on_engraving_change();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS refresh_knife_price_on_knife_update();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS set_knife_price_on_insert();");
        }
    }
}
