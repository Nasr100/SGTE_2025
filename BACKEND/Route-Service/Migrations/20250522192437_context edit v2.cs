using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route_Service.Migrations
{
    /// <inheritdoc />
    public partial class contexteditv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteStop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
