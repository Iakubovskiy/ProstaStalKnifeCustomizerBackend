using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewDbStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_BladeCoatingColors_BladeCoatingColorId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_BladeShapes_ShapeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_HandleColors_HandleColorId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_FasteningId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SheathColors_SheathColorId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "HandleColors");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_BladeCoatingColors_ColorCode",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "ColorMapUrl",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "NormalMapUrl",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "RoughnessMapUrl",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModelUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "pictureUrl",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "BladeShapePhotoUrl",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "bladeShapeModelUrl",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "sheathModelUrl",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "ColorMapUrl",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "NormalMapUrl",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "RoughnessMapUrl",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "BladeCoatingColors");

            migrationBuilder.RenameColumn(
                name: "ShapeId",
                table: "Product",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "HandleColorId",
                table: "Product",
                newName: "SheathId");

            migrationBuilder.RenameColumn(
                name: "FasteningId",
                table: "Product",
                newName: "ModelId");

            migrationBuilder.RenameColumn(
                name: "BladeCoatingColorId",
                table: "Product",
                newName: "KnifeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ShapeId",
                table: "Product",
                newName: "IX_Product_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_HandleColorId",
                table: "Product",
                newName: "IX_Product_SheathId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_FasteningId",
                table: "Product",
                newName: "IX_Product_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BladeCoatingColorId",
                table: "Product",
                newName: "IX_Product_KnifeId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "ClientData_Email");

            migrationBuilder.RenameColumn(
                name: "CountryForDelivery",
                table: "Orders",
                newName: "ClientData_CountryForDelivery");

            migrationBuilder.RenameColumn(
                name: "ClientPhoneNumber",
                table: "Orders",
                newName: "ClientData_ClientPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ClientFullName",
                table: "Orders",
                newName: "ClientData_ClientFullName");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Orders",
                newName: "ClientData_City");

            migrationBuilder.RenameColumn(
                name: "scaleZ",
                table: "Engravings",
                newName: "EngravingScale_ScaleZ");

            migrationBuilder.RenameColumn(
                name: "scaleY",
                table: "Engravings",
                newName: "EngravingScale_ScaleY");

            migrationBuilder.RenameColumn(
                name: "scaleX",
                table: "Engravings",
                newName: "EngravingScale_ScaleX");

            migrationBuilder.RenameColumn(
                name: "rotationZ",
                table: "Engravings",
                newName: "EngravingRotation_RotationZ");

            migrationBuilder.RenameColumn(
                name: "rotationY",
                table: "Engravings",
                newName: "EngravingRotation_RotationY");

            migrationBuilder.RenameColumn(
                name: "rotationX",
                table: "Engravings",
                newName: "EngravingRotation_RotationX");

            migrationBuilder.RenameColumn(
                name: "locationZ",
                table: "Engravings",
                newName: "EngravingPosition_LocationZ");

            migrationBuilder.RenameColumn(
                name: "locationY",
                table: "Engravings",
                newName: "EngravingPosition_LocationY");

            migrationBuilder.RenameColumn(
                name: "locationX",
                table: "Engravings",
                newName: "EngravingPosition_LocationX");

            migrationBuilder.RenameColumn(
                name: "totalLength",
                table: "BladeShapes",
                newName: "BladeCharacteristics_TotalLength");

            migrationBuilder.RenameColumn(
                name: "sharpeningAngle",
                table: "BladeShapes",
                newName: "BladeCharacteristics_SharpeningAngle");

            migrationBuilder.RenameColumn(
                name: "rockwellHardnessUnits",
                table: "BladeShapes",
                newName: "BladeCharacteristics_RockwellHardnessUnits");

            migrationBuilder.RenameColumn(
                name: "bladeWidth",
                table: "BladeShapes",
                newName: "BladeCharacteristics_BladeWidth");

            migrationBuilder.RenameColumn(
                name: "bladeWeight",
                table: "BladeShapes",
                newName: "BladeCharacteristics_BladeWeight");

            migrationBuilder.RenameColumn(
                name: "bladeLength",
                table: "BladeShapes",
                newName: "BladeCharacteristics_BladeLength");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "EngravingColorCode",
                table: "SheathColors",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "SheathColors",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ColorMapId",
                table: "SheathColors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color_TranslationDictionary",
                table: "SheathColors",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Material_TranslationDictionary",
                table: "SheathColors",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TextureId",
                table: "SheathColors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BladeId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "HandleId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Material_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title_TranslationDictionary",
                table: "Product",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Orders""
                ALTER COLUMN ""Number"" TYPE integer
                USING CASE 
                    WHEN ""Number"" ~ '^ORD-[0-9]+$' THEN 
                        LEFT(REPLACE(""Number"", 'ORD-', ''), 3)::integer
                    WHEN ""Number"" ~ '^[0-9]+$' THEN 
                        CASE 
                            WHEN LENGTH(""Number"") > 9 THEN LEFT(""Number"", 9)::integer
                            ELSE ""Number""::integer
                        END
                    ELSE NULL
                END;
            ");
            migrationBuilder.AlterColumn<string>(
                name: "ClientData_Email",
                table: "Orders",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClientData_CountryForDelivery",
                table: "Orders",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClientData_ClientPhoneNumber",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClientData_ClientFullName",
                table: "Orders",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClientData_City",
                table: "Orders",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ClientData_Address",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientData_ZipCode",
                table: "Orders",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Engravings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Font",
                table: "Engravings",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_TranslationDictionary",
                table: "Engravings",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Engravings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name_TranslationDictionary",
                table: "Engravings",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                table: "Engravings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment_TranslationDictionary",
                table: "DeliveryTypes",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_TranslationDictionary",
                table: "DeliveryTypes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "BladeCharacteristics_RockwellHardnessUnits",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BladeShapeModelId",
                table: "BladeShapes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BladeShapePhotoId",
                table: "BladeShapes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_TranslationDictionary",
                table: "BladeShapes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SheathId",
                table: "BladeShapes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "BladeShapes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ColorMapId",
                table: "BladeCoatingColors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color_TranslationDictionary",
                table: "BladeCoatingColors",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TextureId",
                table: "BladeCoatingColors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_TranslationDictionary",
                table: "BladeCoatingColors",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "AttachmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BladeShapeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BladeShapeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngravingTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    EngravingId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngravingTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngravingTags_Engravings_EngravingId",
                        column: x => x.EngravingId,
                        principalTable: "Engravings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FileEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Description_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag_TranslationDictionary = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTag_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SheathColorPriceByType",
                columns: table => new
                {
                    SheathColorId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheathColorPriceByType", x => new { x.TypeId, x.SheathColorId });
                    table.ForeignKey(
                        name: "FK_SheathColorPriceByType_BladeShapeTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BladeShapeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheathColorPriceByType_SheathColors_SheathColorId",
                        column: x => x.SheathColorId,
                        principalTable: "SheathColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sheaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheaths_BladeShapeTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BladeShapeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sheaths_FileEntity_ModelId",
                        column: x => x.ModelId,
                        principalTable: "FileEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Textures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NormalMapId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoughnessMapId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Textures_FileEntity_NormalMapId",
                        column: x => x.NormalMapId,
                        principalTable: "FileEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Textures_FileEntity_RoughnessMapId",
                        column: x => x.RoughnessMapId,
                        principalTable: "FileEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Handles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    ColorCode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Material_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    TextureId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorMapId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    HandleModelId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handles_FileEntity_ColorMapId",
                        column: x => x.ColorMapId,
                        principalTable: "FileEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Handles_FileEntity_HandleModelId",
                        column: x => x.HandleModelId,
                        principalTable: "FileEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Handles_Textures_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Textures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheathColors_ColorMapId",
                table: "SheathColors",
                column: "ColorMapId");

            migrationBuilder.CreateIndex(
                name: "IX_SheathColors_TextureId",
                table: "SheathColors",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BladeId",
                table: "Product",
                column: "BladeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ColorId",
                table: "Product",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_HandleId",
                table: "Product",
                column: "HandleId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageId",
                table: "Product",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Engravings_PictureId",
                table: "Engravings",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeShapes_BladeShapeModelId",
                table: "BladeShapes",
                column: "BladeShapeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeShapes_BladeShapePhotoId",
                table: "BladeShapes",
                column: "BladeShapePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeShapes_SheathId",
                table: "BladeShapes",
                column: "SheathId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeShapes_TypeId",
                table: "BladeShapes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeCoatingColors_ColorMapId",
                table: "BladeCoatingColors",
                column: "ColorMapId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeCoatingColors_TextureId",
                table: "BladeCoatingColors",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_EngravingTags_EngravingId",
                table: "EngravingTags",
                column: "EngravingId");

            migrationBuilder.CreateIndex(
                name: "IX_Handles_ColorMapId",
                table: "Handles",
                column: "ColorMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Handles_HandleModelId",
                table: "Handles",
                column: "HandleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Handles_TextureId",
                table: "Handles",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_ProductId",
                table: "ProductTag",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SheathColorPriceByType_SheathColorId",
                table: "SheathColorPriceByType",
                column: "SheathColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheaths_ModelId",
                table: "Sheaths",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheaths_TypeId",
                table: "Sheaths",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Textures_NormalMapId",
                table: "Textures",
                column: "NormalMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Textures_RoughnessMapId",
                table: "Textures",
                column: "RoughnessMapId");

            migrationBuilder.AddForeignKey(
                name: "FK_BladeCoatingColors_FileEntity_ColorMapId",
                table: "BladeCoatingColors",
                column: "ColorMapId",
                principalTable: "FileEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BladeCoatingColors_Textures_TextureId",
                table: "BladeCoatingColors",
                column: "TextureId",
                principalTable: "Textures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BladeShapes_BladeShapeTypes_TypeId",
                table: "BladeShapes",
                column: "TypeId",
                principalTable: "BladeShapeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BladeShapes_FileEntity_BladeShapeModelId",
                table: "BladeShapes",
                column: "BladeShapeModelId",
                principalTable: "FileEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BladeShapes_FileEntity_BladeShapePhotoId",
                table: "BladeShapes",
                column: "BladeShapePhotoId",
                principalTable: "FileEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BladeShapes_Sheaths_SheathId",
                table: "BladeShapes",
                column: "SheathId",
                principalTable: "Sheaths",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Engravings_FileEntity_PictureId",
                table: "Engravings",
                column: "PictureId",
                principalTable: "FileEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AttachmentTypes_TypeId",
                table: "Product",
                column: "TypeId",
                principalTable: "AttachmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BladeCoatingColors_ColorId",
                table: "Product",
                column: "ColorId",
                principalTable: "BladeCoatingColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BladeShapes_BladeId",
                table: "Product",
                column: "BladeId",
                principalTable: "BladeShapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FileEntity_ImageId",
                table: "Product",
                column: "ImageId",
                principalTable: "FileEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FileEntity_ModelId",
                table: "Product",
                column: "ModelId",
                principalTable: "FileEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Handles_HandleId",
                table: "Product",
                column: "HandleId",
                principalTable: "Handles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_KnifeId",
                table: "Product",
                column: "KnifeId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SheathColors_SheathColorId",
                table: "Product",
                column: "SheathColorId",
                principalTable: "SheathColors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Sheaths_SheathId",
                table: "Product",
                column: "SheathId",
                principalTable: "Sheaths",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SheathColors_FileEntity_ColorMapId",
                table: "SheathColors",
                column: "ColorMapId",
                principalTable: "FileEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SheathColors_Textures_TextureId",
                table: "SheathColors",
                column: "TextureId",
                principalTable: "Textures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BladeCoatingColors_FileEntity_ColorMapId",
                table: "BladeCoatingColors");

            migrationBuilder.DropForeignKey(
                name: "FK_BladeCoatingColors_Textures_TextureId",
                table: "BladeCoatingColors");

            migrationBuilder.DropForeignKey(
                name: "FK_BladeShapes_BladeShapeTypes_TypeId",
                table: "BladeShapes");

            migrationBuilder.DropForeignKey(
                name: "FK_BladeShapes_FileEntity_BladeShapeModelId",
                table: "BladeShapes");

            migrationBuilder.DropForeignKey(
                name: "FK_BladeShapes_FileEntity_BladeShapePhotoId",
                table: "BladeShapes");

            migrationBuilder.DropForeignKey(
                name: "FK_BladeShapes_Sheaths_SheathId",
                table: "BladeShapes");

            migrationBuilder.DropForeignKey(
                name: "FK_Engravings_FileEntity_PictureId",
                table: "Engravings");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AttachmentTypes_TypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_BladeCoatingColors_ColorId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_BladeShapes_BladeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_FileEntity_ImageId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_FileEntity_ModelId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Handles_HandleId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_KnifeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SheathColors_SheathColorId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Sheaths_SheathId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_SheathColors_FileEntity_ColorMapId",
                table: "SheathColors");

            migrationBuilder.DropForeignKey(
                name: "FK_SheathColors_Textures_TextureId",
                table: "SheathColors");

            migrationBuilder.DropTable(
                name: "AttachmentTypes");

            migrationBuilder.DropTable(
                name: "EngravingTags");

            migrationBuilder.DropTable(
                name: "Handles");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "SheathColorPriceByType");

            migrationBuilder.DropTable(
                name: "Sheaths");

            migrationBuilder.DropTable(
                name: "Textures");

            migrationBuilder.DropTable(
                name: "BladeShapeTypes");

            migrationBuilder.DropTable(
                name: "FileEntity");

            migrationBuilder.DropIndex(
                name: "IX_SheathColors_ColorMapId",
                table: "SheathColors");

            migrationBuilder.DropIndex(
                name: "IX_SheathColors_TextureId",
                table: "SheathColors");

            migrationBuilder.DropIndex(
                name: "IX_Product_BladeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ColorId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_HandleId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ImageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Engravings_PictureId",
                table: "Engravings");

            migrationBuilder.DropIndex(
                name: "IX_BladeShapes_BladeShapeModelId",
                table: "BladeShapes");

            migrationBuilder.DropIndex(
                name: "IX_BladeShapes_BladeShapePhotoId",
                table: "BladeShapes");

            migrationBuilder.DropIndex(
                name: "IX_BladeShapes_SheathId",
                table: "BladeShapes");

            migrationBuilder.DropIndex(
                name: "IX_BladeShapes_TypeId",
                table: "BladeShapes");

            migrationBuilder.DropIndex(
                name: "IX_BladeCoatingColors_ColorMapId",
                table: "BladeCoatingColors");

            migrationBuilder.DropIndex(
                name: "IX_BladeCoatingColors_TextureId",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "ColorMapId",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "Color_TranslationDictionary",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "Material_TranslationDictionary",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "TextureId",
                table: "SheathColors");

            migrationBuilder.DropColumn(
                name: "BladeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Color_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "HandleId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Material_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MetaDescription_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MetaTitle_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Name_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Title_TranslationDictionary",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ClientData_Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientData_ZipCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Description_TranslationDictionary",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "Name_TranslationDictionary",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Engravings");

            migrationBuilder.DropColumn(
                name: "Comment_TranslationDictionary",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "Name_TranslationDictionary",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "BladeShapeModelId",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "BladeShapePhotoId",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "Name_TranslationDictionary",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "SheathId",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "ColorMapId",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "Color_TranslationDictionary",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "TextureId",
                table: "BladeCoatingColors");

            migrationBuilder.DropColumn(
                name: "Type_TranslationDictionary",
                table: "BladeCoatingColors");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Product",
                newName: "ShapeId");

            migrationBuilder.RenameColumn(
                name: "SheathId",
                table: "Product",
                newName: "HandleColorId");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Product",
                newName: "FasteningId");

            migrationBuilder.RenameColumn(
                name: "KnifeId",
                table: "Product",
                newName: "BladeCoatingColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_TypeId",
                table: "Product",
                newName: "IX_Product_ShapeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_SheathId",
                table: "Product",
                newName: "IX_Product_HandleColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ModelId",
                table: "Product",
                newName: "IX_Product_FasteningId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_KnifeId",
                table: "Product",
                newName: "IX_Product_BladeCoatingColorId");

            migrationBuilder.RenameColumn(
                name: "ClientData_Email",
                table: "Orders",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ClientData_CountryForDelivery",
                table: "Orders",
                newName: "CountryForDelivery");

            migrationBuilder.RenameColumn(
                name: "ClientData_ClientPhoneNumber",
                table: "Orders",
                newName: "ClientPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ClientData_ClientFullName",
                table: "Orders",
                newName: "ClientFullName");

            migrationBuilder.RenameColumn(
                name: "ClientData_City",
                table: "Orders",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "EngravingScale_ScaleZ",
                table: "Engravings",
                newName: "scaleZ");

            migrationBuilder.RenameColumn(
                name: "EngravingScale_ScaleY",
                table: "Engravings",
                newName: "scaleY");

            migrationBuilder.RenameColumn(
                name: "EngravingScale_ScaleX",
                table: "Engravings",
                newName: "scaleX");

            migrationBuilder.RenameColumn(
                name: "EngravingRotation_RotationZ",
                table: "Engravings",
                newName: "rotationZ");

            migrationBuilder.RenameColumn(
                name: "EngravingRotation_RotationY",
                table: "Engravings",
                newName: "rotationY");

            migrationBuilder.RenameColumn(
                name: "EngravingRotation_RotationX",
                table: "Engravings",
                newName: "rotationX");

            migrationBuilder.RenameColumn(
                name: "EngravingPosition_LocationZ",
                table: "Engravings",
                newName: "locationZ");

            migrationBuilder.RenameColumn(
                name: "EngravingPosition_LocationY",
                table: "Engravings",
                newName: "locationY");

            migrationBuilder.RenameColumn(
                name: "EngravingPosition_LocationX",
                table: "Engravings",
                newName: "locationX");

            migrationBuilder.RenameColumn(
                name: "BladeCharacteristics_TotalLength",
                table: "BladeShapes",
                newName: "totalLength");

            migrationBuilder.RenameColumn(
                name: "BladeCharacteristics_SharpeningAngle",
                table: "BladeShapes",
                newName: "sharpeningAngle");

            migrationBuilder.RenameColumn(
                name: "BladeCharacteristics_RockwellHardnessUnits",
                table: "BladeShapes",
                newName: "rockwellHardnessUnits");

            migrationBuilder.RenameColumn(
                name: "BladeCharacteristics_BladeWidth",
                table: "BladeShapes",
                newName: "bladeWidth");

            migrationBuilder.RenameColumn(
                name: "BladeCharacteristics_BladeWeight",
                table: "BladeShapes",
                newName: "bladeWeight");

            migrationBuilder.RenameColumn(
                name: "BladeCharacteristics_BladeLength",
                table: "BladeShapes",
                newName: "bladeLength");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "EngravingColorCode",
                table: "SheathColors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "SheathColors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "SheathColors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorMapUrl",
                table: "SheathColors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "SheathColors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalMapUrl",
                table: "SheathColors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "SheathColors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RoughnessMapUrl",
                table: "SheathColors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelUrl",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CountryForDelivery",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "ClientPhoneNumber",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ClientFullName",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Engravings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Font",
                table: "Engravings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Engravings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pictureUrl",
                table: "Engravings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "DeliveryTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeliveryTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "rockwellHardnessUnits",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "BladeShapePhotoUrl",
                table: "BladeShapes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bladeShapeModelUrl",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sheathModelUrl",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "BladeCoatingColors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorMapUrl",
                table: "BladeCoatingColors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalMapUrl",
                table: "BladeCoatingColors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughnessMapUrl",
                table: "BladeCoatingColors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "BladeCoatingColors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HandleColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ColorCode = table.Column<string>(type: "text", nullable: true),
                    ColorMapUrl = table.Column<string>(type: "text", nullable: true),
                    ColorName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Material = table.Column<string>(type: "text", nullable: false),
                    NormalMapUrl = table.Column<string>(type: "text", nullable: true),
                    RoughnessMapUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandleColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeCoatingColors_ColorCode",
                table: "BladeCoatingColors",
                column: "ColorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BladeCoatingColors_BladeCoatingColorId",
                table: "Product",
                column: "BladeCoatingColorId",
                principalTable: "BladeCoatingColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BladeShapes_ShapeId",
                table: "Product",
                column: "ShapeId",
                principalTable: "BladeShapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_HandleColors_HandleColorId",
                table: "Product",
                column: "HandleColorId",
                principalTable: "HandleColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_FasteningId",
                table: "Product",
                column: "FasteningId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SheathColors_SheathColorId",
                table: "Product",
                column: "SheathColorId",
                principalTable: "SheathColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
