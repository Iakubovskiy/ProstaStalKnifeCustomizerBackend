using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUserStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserTokens"" DROP CONSTRAINT IF EXISTS ""FK_AspNetUserTokens_AspNetUsers_UserId"";");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserClaims"" DROP CONSTRAINT IF EXISTS ""FK_AspNetUserClaims_AspNetUsers_UserId"";");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserLogins"" DROP CONSTRAINT IF EXISTS ""FK_AspNetUserLogins_AspNetUsers_UserId"";");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserRoles"" DROP CONSTRAINT IF EXISTS ""FK_AspNetUserRoles_AspNetUsers_UserId"";");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserRoles"" DROP CONSTRAINT IF EXISTS ""FK_AspNetUserRoles_AspNetRoles_RoleId"";");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetRoleClaims"" DROP CONSTRAINT IF EXISTS ""FK_AspNetRoleClaims_AspNetRoles_RoleId"";");

            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUsers"" ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserTokens"" ALTER COLUMN ""UserId"" TYPE uuid USING ""UserId""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserClaims"" ALTER COLUMN ""UserId"" TYPE uuid USING ""UserId""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserLogins"" ALTER COLUMN ""UserId"" TYPE uuid USING ""UserId""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserRoles"" ALTER COLUMN ""UserId"" TYPE uuid USING ""UserId""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetUserRoles"" ALTER COLUMN ""RoleId"" TYPE uuid USING ""RoleId""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetRoles"" ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""AspNetRoleClaims"" ALTER COLUMN ""RoleId"" TYPE uuid USING ""RoleId""::uuid;");

            migrationBuilder.AddColumn<string>(
                name: "UserData_Address",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserData_City",
                table: "AspNetUsers",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserData_ClientFullName",
                table: "AspNetUsers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserData_ClientPhoneNumber",
                table: "AspNetUsers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserData_CountryForDelivery",
                table: "AspNetUsers",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserData_Email",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserData_ZipCode",
                table: "AspNetUsers",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.Sql(@"
                ALTER TABLE ""AspNetUserTokens""
                ADD CONSTRAINT ""FK_AspNetUserTokens_AspNetUsers_UserId""
                FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers""(""Id"") ON DELETE CASCADE;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""AspNetUserClaims""
                ADD CONSTRAINT ""FK_AspNetUserClaims_AspNetUsers_UserId""
                FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers""(""Id"") ON DELETE CASCADE;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""AspNetUserLogins""
                ADD CONSTRAINT ""FK_AspNetUserLogins_AspNetUsers_UserId""
                FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers""(""Id"") ON DELETE CASCADE;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""AspNetUserRoles""
                ADD CONSTRAINT ""FK_AspNetUserRoles_AspNetUsers_UserId""
                FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers""(""Id"") ON DELETE CASCADE;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""AspNetUserRoles""
                ADD CONSTRAINT ""FK_AspNetUserRoles_AspNetRoles_RoleId""
                FOREIGN KEY (""RoleId"") REFERENCES ""AspNetRoles""(""Id"") ON DELETE CASCADE;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""AspNetRoleClaims""
                ADD CONSTRAINT ""FK_AspNetRoleClaims_AspNetRoles_RoleId""
                FOREIGN KEY (""RoleId"") REFERENCES ""AspNetRoles""(""Id"") ON DELETE CASCADE;
            ");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserData_Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserData_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserData_ClientFullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserData_ClientPhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserData_CountryForDelivery",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserData_Email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserData_ZipCode",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
