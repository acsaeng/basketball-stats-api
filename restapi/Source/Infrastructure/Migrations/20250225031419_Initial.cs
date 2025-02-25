using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballStatsApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dob = table.Column<DateOnly>(type: "date", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InjuryStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RosterStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JerseyNumber = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    Assists = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    Rebounds = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    Steals = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    Blocks = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    Turnovers = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
