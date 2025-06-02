using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route_Service.Migrations
{
    /// <inheritdoc />
    public partial class contextedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Stops_stop_id",
                table: "RouteStops");

            migrationBuilder.CreateTable(
                name: "RouteStop",
                columns: table => new
                {
                    RoutesId = table.Column<int>(type: "int", nullable: false),
                    StopsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStop", x => new { x.RoutesId, x.StopsId });
                    table.ForeignKey(
                        name: "FK_RouteStop_Routes_RoutesId",
                        column: x => x.RoutesId,
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStop_Stops_StopsId",
                        column: x => x.StopsId,
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStop_StopsId",
                table: "RouteStop",
                column: "StopsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Stops_stop_id",
                table: "RouteStops",
                column: "stop_id",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Stops_stop_id",
                table: "RouteStops");

            migrationBuilder.DropTable(
                name: "RouteStop");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Stops_stop_id",
                table: "RouteStops",
                column: "stop_id",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
