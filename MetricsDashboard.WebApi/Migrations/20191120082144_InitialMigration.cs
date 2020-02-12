using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MetricsDashboard.WebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Limit = table.Column<int>(nullable: false),
                    Wish = table.Column<int>(nullable: true),
                    Goal = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetricHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    MetricId = table.Column<int>(nullable: true),
                    AddedOn = table.Column<DateTimeOffset>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetricHistory_Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "Metrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetricHistory_MetricId",
                table: "MetricHistory",
                column: "MetricId");

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[Metrics] ([Name], [Type], [Limit], [Wish], [Goal])
                    VALUES 
                    (N'BE Unit Test Coverate', 1, 450, 750, 600),
                    (N'BE Build Time', 3, 600, null, 300),
                    (N'FE Build Time', 3, 900, null, 300),
                    (N'Monthly Azure Cost', 2, 500, 300, 400);
                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricHistory");

            migrationBuilder.DropTable(
                name: "Metrics");
        }
    }
}
