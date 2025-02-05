using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appUsers",
                columns: table => new
                {
                    dbId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IsBot = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPremium = table.Column<bool>(type: "bit", nullable: false),
                    AddedToAttachmentMenu = table.Column<bool>(type: "bit", nullable: false),
                    CanJoinGroups = table.Column<bool>(type: "bit", nullable: false),
                    CanReadAllGroupMessages = table.Column<bool>(type: "bit", nullable: false),
                    SupportsInlineQueries = table.Column<bool>(type: "bit", nullable: false),
                    CanConnectToBusiness = table.Column<bool>(type: "bit", nullable: false),
                    HasMainWebApp = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appUsers", x => x.dbId);
                });

            migrationBuilder.CreateTable(
                name: "citys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    sub = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CallBackId = table.Column<int>(type: "int", maxLength: 110, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastChecked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextChek = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscriptions_appUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "appUsers",
                        principalColumn: "dbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptions_citys_CityId",
                        column: x => x.CityId,
                        principalTable: "citys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "vacancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vacancies_appUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "appUsers",
                        principalColumn: "dbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vacancies_citys_CityId",
                        column: x => x.CityId,
                        principalTable: "citys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionsVacancies",
                columns: table => new
                {
                    subscriptionsId = table.Column<int>(type: "int", nullable: false),
                    vacanciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionsVacancies", x => new { x.subscriptionsId, x.vacanciesId });
                    table.ForeignKey(
                        name: "FK_SubscriptionsVacancies_subscriptions_subscriptionsId",
                        column: x => x.subscriptionsId,
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionsVacancies_vacancies_vacanciesId",
                        column: x => x.vacanciesId,
                        principalTable: "vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_AppUserId",
                table: "subscriptions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_CityId",
                table: "subscriptions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsVacancies_vacanciesId",
                table: "SubscriptionsVacancies",
                column: "vacanciesId");

            migrationBuilder.CreateIndex(
                name: "IX_vacancies_AppUserId",
                table: "vacancies",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_vacancies_CityId",
                table: "vacancies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_vacancies_URL_SiteName",
                table: "vacancies",
                columns: new[] { "URL", "SiteName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionsVacancies");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "vacancies");

            migrationBuilder.DropTable(
                name: "appUsers");

            migrationBuilder.DropTable(
                name: "citys");
        }
    }
}
