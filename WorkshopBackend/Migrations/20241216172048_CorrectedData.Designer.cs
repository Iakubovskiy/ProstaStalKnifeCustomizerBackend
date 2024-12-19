﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkshopBackend.Data;

#nullable disable

namespace WorkshopBackend.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20241216172048_CorrectedData")]
    partial class CorrectedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WorkshopBackend.Models.BladeCoating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("BladeCoatings");
                });

            modelBuilder.Entity("WorkshopBackend.Models.BladeCoatingColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BladeCoatingId")
                        .HasColumnType("integer");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EngravingColorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BladeCoatingId");

                    b.HasIndex("ColorCode");

                    b.ToTable("BladeCoatingColors");
                });

            modelBuilder.Entity("WorkshopBackend.Models.BladeShape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<double>("bladeLength")
                        .HasColumnType("double precision");

                    b.Property<string>("bladeShapeModelUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("bladeWeight")
                        .HasColumnType("double precision");

                    b.Property<double>("bladeWidth")
                        .HasColumnType("double precision");

                    b.Property<double>("engravingLocationX")
                        .HasColumnType("double precision");

                    b.Property<double>("engravingLocationY")
                        .HasColumnType("double precision");

                    b.Property<double>("engravingLocationZ")
                        .HasColumnType("double precision");

                    b.Property<double>("engravingRotationX")
                        .HasColumnType("double precision");

                    b.Property<double>("engravingRotationY")
                        .HasColumnType("double precision");

                    b.Property<double>("engravingRotationZ")
                        .HasColumnType("double precision");

                    b.Property<string>("handleShapeModelUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("rockwellHardnessUnits")
                        .HasColumnType("double precision");

                    b.Property<double>("sharpeningAngle")
                        .HasColumnType("double precision");

                    b.Property<string>("sheathModelUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("totalLength")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("BladeShapes");
                });

            modelBuilder.Entity("WorkshopBackend.Models.DeliveryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("DeliveryTypes");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Engraving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Font")
                        .HasColumnType("text");

                    b.Property<int?>("KnifeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Side")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<double>("locationX")
                        .HasColumnType("double precision");

                    b.Property<double>("locationY")
                        .HasColumnType("double precision");

                    b.Property<double>("locationZ")
                        .HasColumnType("double precision");

                    b.Property<string>("pictureUrl")
                        .HasColumnType("text");

                    b.Property<double>("rotationX")
                        .HasColumnType("double precision");

                    b.Property<double>("rotationY")
                        .HasColumnType("double precision");

                    b.Property<double>("rotationZ")
                        .HasColumnType("double precision");

                    b.Property<double>("scaleX")
                        .HasColumnType("double precision");

                    b.Property<double>("scaleY")
                        .HasColumnType("double precision");

                    b.Property<double>("scaleZ")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("KnifeId");

                    b.ToTable("Engravings");
                });

            modelBuilder.Entity("WorkshopBackend.Models.EngravingPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("EngravingPrices");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Fastening", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("KnifeId")
                        .HasColumnType("integer");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("KnifeId");

                    b.ToTable("Fastenings");
                });

            modelBuilder.Entity("WorkshopBackend.Models.HandleColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MaterialUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HandleColors");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Knife", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BladeCoatingColorId")
                        .HasColumnType("integer");

                    b.Property<int>("BladeCoatingId")
                        .HasColumnType("integer");

                    b.Property<int>("HandleColorId")
                        .HasColumnType("integer");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("ShapeId")
                        .HasColumnType("integer");

                    b.Property<int>("SheathColorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BladeCoatingColorId");

                    b.HasIndex("BladeCoatingId");

                    b.HasIndex("HandleColorId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShapeId");

                    b.HasIndex("SheathColorId");

                    b.ToTable("Knives");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientFullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("CountryForDelivery")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DeliveryTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryTypeId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WorkshopBackend.Models.OrderStatuses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("WorkshopBackend.Models.SheathColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MaterialUrl")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("SheathColors");
                });

            modelBuilder.Entity("WorkshopBackend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WorkshopBackend.Models.Admin", b =>
                {
                    b.HasBaseType("WorkshopBackend.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WorkshopBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorkshopBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkshopBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WorkshopBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkshopBackend.Models.BladeCoatingColor", b =>
                {
                    b.HasOne("WorkshopBackend.Models.BladeCoating", null)
                        .WithMany("Colors")
                        .HasForeignKey("BladeCoatingId");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Engraving", b =>
                {
                    b.HasOne("WorkshopBackend.Models.Knife", null)
                        .WithMany("Engravings")
                        .HasForeignKey("KnifeId");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Fastening", b =>
                {
                    b.HasOne("WorkshopBackend.Models.Knife", null)
                        .WithMany("Fastening")
                        .HasForeignKey("KnifeId");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Knife", b =>
                {
                    b.HasOne("WorkshopBackend.Models.BladeCoatingColor", "BladeCoatingColor")
                        .WithMany()
                        .HasForeignKey("BladeCoatingColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkshopBackend.Models.BladeCoating", "BladeCoating")
                        .WithMany()
                        .HasForeignKey("BladeCoatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkshopBackend.Models.HandleColor", "HandleColor")
                        .WithMany()
                        .HasForeignKey("HandleColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkshopBackend.Models.Order", null)
                        .WithMany("Knives")
                        .HasForeignKey("OrderId");

                    b.HasOne("WorkshopBackend.Models.BladeShape", "Shape")
                        .WithMany()
                        .HasForeignKey("ShapeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkshopBackend.Models.SheathColor", "SheathColor")
                        .WithMany()
                        .HasForeignKey("SheathColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BladeCoating");

                    b.Navigation("BladeCoatingColor");

                    b.Navigation("HandleColor");

                    b.Navigation("Shape");

                    b.Navigation("SheathColor");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Order", b =>
                {
                    b.HasOne("WorkshopBackend.Models.DeliveryType", "DeliveryType")
                        .WithMany()
                        .HasForeignKey("DeliveryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkshopBackend.Models.OrderStatuses", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WorkshopBackend.Models.BladeCoating", b =>
                {
                    b.Navigation("Colors");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Knife", b =>
                {
                    b.Navigation("Engravings");

                    b.Navigation("Fastening");
                });

            modelBuilder.Entity("WorkshopBackend.Models.Order", b =>
                {
                    b.Navigation("Knives");
                });
#pragma warning restore 612, 618
        }
    }
}
