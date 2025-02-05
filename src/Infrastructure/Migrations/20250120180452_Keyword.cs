using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Keyword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_citys_CityId",
                table: "subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Keywords",
                table: "subscriptions",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_citys_CityId",
                table: "subscriptions",
                column: "CityId",
                principalTable: "citys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_citys_CityId",
                table: "subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Keywords",
                table: "subscriptions",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_citys_CityId",
                table: "subscriptions",
                column: "CityId",
                principalTable: "citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
