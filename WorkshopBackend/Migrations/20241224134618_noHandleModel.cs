using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopBackend.Migrations
{
    /// <inheritdoc />
    public partial class noHandleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "handleLocationX",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleLocationY",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleLocationZ",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleRotationX",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleRotationY",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleRotationZ",
                table: "BladeShapes");

            migrationBuilder.DropColumn(
                name: "handleShapeModelUrl",
                table: "BladeShapes");

            migrationBuilder.AlterColumn<double>(
                name: "rockwellHardnessUnits",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "engravingRotationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "engravingRotationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "engravingRotationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "engravingLocationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "engravingLocationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "engravingLocationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "rockwellHardnessUnits",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "engravingRotationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "engravingRotationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "engravingRotationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "engravingLocationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "engravingLocationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "engravingLocationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleLocationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleLocationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleLocationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleRotationX",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleRotationY",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "handleRotationZ",
                table: "BladeShapes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "handleShapeModelUrl",
                table: "BladeShapes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
