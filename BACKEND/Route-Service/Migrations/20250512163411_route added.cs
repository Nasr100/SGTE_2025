using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route_Service.Migrations
{
    /// <inheritdoc />
    public partial class routeadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "capacity",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "route_id",
                table: "Buses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_activate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    stop_id = table.Column<int>(type: "int", nullable: false),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    stop_order = table.Column<int>(type: "int", nullable: false),
                    arrival_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    departure_time = table.Column<TimeOnly>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => new { x.route_id, x.stop_id });
                    table.ForeignKey(
                        name: "FK_RouteStops_Routes_route_id",
                        column: x => x.route_id,
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStops_Stops_stop_id",
                        column: x => x.stop_id,
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_route_id",
                table: "Buses",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_stop_id",
                table: "RouteStops",
                column: "stop_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Routes_route_id",
                table: "Buses",
                column: "route_id",
                principalTable: "Routes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Routes_route_id",
                table: "Buses");

            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Buses_route_id",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "capacity",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "route_id",
                table: "Buses");
        }
    }
}
