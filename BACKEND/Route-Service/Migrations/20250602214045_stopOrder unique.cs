using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route_Service.Migrations
{
    /// <inheritdoc />
    public partial class stopOrderunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_stop_order",
                table: "RouteStops",
                column: "stop_order",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RouteStops_stop_order",
                table: "RouteStops");
        }
    }
}
