using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductTagConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Приводимо тип через ручний SQL
            migrationBuilder.Sql("""
                                     ALTER TABLE "ProductTag"
                                     ALTER COLUMN "Tag_TranslationDictionary"
                                     TYPE jsonb
                                     USING "Tag_TranslationDictionary"::jsonb;
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     ALTER TABLE "ProductTag"
                                     ALTER COLUMN "Tag_TranslationDictionary"
                                     TYPE hstore
                                     USING "Tag_TranslationDictionary"::hstore;
                                 """);
        }

    }
}
