using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Comment_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngravingPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngravingPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engravings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Side = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Font = table.Column<string>(type: "text", nullable: true),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    EngravingPosition_LocationX = table.Column<double>(type: "double precision", nullable: false),
                    EngravingPosition_LocationY = table.Column<double>(type: "double precision", nullable: false),
                    EngravingPosition_LocationZ = table.Column<double>(type: "double precision", nullable: false),
                    EngravingRotation_RotationX = table.Column<double>(type: "double precision", nullable: false),
                    EngravingRotation_RotationY = table.Column<double>(type: "double precision", nullable: false),
                    EngravingRotation_RotationZ = table.Column<double>(type: "double precision", nullable: false),
                    EngravingScale_ScaleX = table.Column<double>(type: "double precision", nullable: false),
                    EngravingScale_ScaleY = table.Column<double>(type: "double precision", nullable: false),
                    EngravingScale_ScaleZ = table.Column<double>(type: "double precision", nullable: false),
                    Description_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engravings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Description_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Textures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalMapUrl = table.Column<string>(type: "text", nullable: false),
                    RoughnessMapUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BladeShapes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    BladeShapePhotoUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    BladeCharacteristics_TotalLength = table.Column<double>(type: "double precision", nullable: false),
                    BladeCharacteristics_BladeLength = table.Column<double>(type: "double precision", nullable: false),
                    BladeCharacteristics_BladeWidth = table.Column<double>(type: "double precision", nullable: false),
                    BladeCharacteristics_BladeWeight = table.Column<double>(type: "double precision", nullable: false),
                    BladeCharacteristics_SharpeningAngle = table.Column<double>(type: "double precision", nullable: false),
                    BladeCharacteristics_RockwellHardnessUnits = table.Column<double>(type: "double precision", nullable: false),
                    BladeShapeModelUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SheathModelUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BladeShapes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BladeShapes_BladeShapeTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BladeShapeTypes",
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
                    ModelUrl = table.Column<string>(type: "text", nullable: true),
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<double>(type: "double precision", nullable: false),
                    DeliveryTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientData_ClientFullName = table.Column<string>(type: "text", nullable: false),
                    ClientData_ClientPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ClientData_CountryForDelivery = table.Column<string>(type: "text", nullable: false),
                    ClientData_City = table.Column<string>(type: "text", nullable: false),
                    ClientData_Email = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryTypes_DeliveryTypeId",
                        column: x => x.DeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BladeCoatingColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    Color_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    ColorCode = table.Column<string>(type: "text", nullable: true),
                    EngravingColorCode = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    TextureId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorMapUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BladeCoatingColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BladeCoatingColors_Textures_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Textures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Handles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    ColorCode = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Material_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    TextureId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorMapUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    HandleModelUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handles_Textures_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Textures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SheathColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Material = table.Column<string>(type: "text", nullable: false),
                    EngravingColorCode = table.Column<string>(type: "text", nullable: false),
                    TextureId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorMapUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheathColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheathColors_Textures_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Textures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Name_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Title_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Description_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    MetaTitle_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    MetaDescription_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Color_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    Material_TranslationDictionary = table.Column<string>(type: "jsonb", nullable: true),
                    ModelUrl = table.Column<string>(type: "text", nullable: true),
                    KnifeId = table.Column<Guid>(type: "uuid", nullable: true),
                    BladeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorId = table.Column<Guid>(type: "uuid", nullable: true),
                    HandleId = table.Column<Guid>(type: "uuid", nullable: true),
                    SheathId = table.Column<Guid>(type: "uuid", nullable: true),
                    SheathColorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_BladeCoatingColors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "BladeCoatingColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_BladeShapes_BladeId",
                        column: x => x.BladeId,
                        principalTable: "BladeShapes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Handles_HandleId",
                        column: x => x.HandleId,
                        principalTable: "Handles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Product_KnifeId",
                        column: x => x.KnifeId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_SheathColors_SheathColorId",
                        column: x => x.SheathColorId,
                        principalTable: "SheathColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Sheaths_SheathId",
                        column: x => x.SheathId,
                        principalTable: "Sheaths",
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
                name: "EngravingKnife",
                columns: table => new
                {
                    EngravingsId = table.Column<Guid>(type: "uuid", nullable: false),
                    KnifeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngravingKnife", x => new { x.EngravingsId, x.KnifeId });
                    table.ForeignKey(
                        name: "FK_EngravingKnife_Engravings_EngravingsId",
                        column: x => x.EngravingsId,
                        principalTable: "Engravings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngravingKnife_Product_KnifeId",
                        column: x => x.KnifeId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BladeCoatingColors_TextureId",
                table: "BladeCoatingColors",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_BladeShapes_TypeId",
                table: "BladeShapes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngravingKnife_KnifeId",
                table: "EngravingKnife",
                column: "KnifeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngravingTags_EngravingId",
                table: "EngravingTags",
                column: "EngravingId");

            migrationBuilder.CreateIndex(
                name: "IX_Handles_TextureId",
                table: "Handles",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

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
                name: "IX_Product_KnifeId",
                table: "Product",
                column: "KnifeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SheathColorId",
                table: "Product",
                column: "SheathColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SheathId",
                table: "Product",
                column: "SheathId");

            migrationBuilder.CreateIndex(
                name: "IX_SheathColorPriceByType_SheathColorId",
                table: "SheathColorPriceByType",
                column: "SheathColorId");

            migrationBuilder.CreateIndex(
                name: "IX_SheathColors_TextureId",
                table: "SheathColors",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheaths_TypeId",
                table: "Sheaths",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EngravingKnife");

            migrationBuilder.DropTable(
                name: "EngravingPrices");

            migrationBuilder.DropTable(
                name: "EngravingTags");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "SheathColorPriceByType");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Engravings");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "BladeCoatingColors");

            migrationBuilder.DropTable(
                name: "BladeShapes");

            migrationBuilder.DropTable(
                name: "Handles");

            migrationBuilder.DropTable(
                name: "SheathColors");

            migrationBuilder.DropTable(
                name: "Sheaths");

            migrationBuilder.DropTable(
                name: "Textures");

            migrationBuilder.DropTable(
                name: "BladeShapeTypes");
        }
    }
}
