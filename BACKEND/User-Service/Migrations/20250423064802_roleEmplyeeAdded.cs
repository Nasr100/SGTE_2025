using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Service.Migrations
{
    /// <inheritdoc />
    public partial class roleEmplyeeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrations_Employees_employee_id",
                table: "Administrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Employees_EmployeeId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Administrations_employee_id",
                table: "Administrations");

            migrationBuilder.DropColumn(
                name: "bus_id",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "bus_id",
                table: "Administrations");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Drivers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Drivers",
                newName: "employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_EmployeeId",
                table: "Drivers",
                newName: "IX_Drivers_employee_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Administrations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "seat_number",
                table: "Administrations",
                newName: "Employeeid");

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "Workers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles_employees",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_employees", x => new { x.EmployeesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_roles_employees_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_employees_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Administrations_Employeeid",
                table: "Administrations",
                column: "Employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_roles_employees_RolesId",
                table: "roles_employees",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrations_Employees_Employeeid",
                table: "Administrations",
                column: "Employeeid",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Employees_employee_id",
                table: "Drivers",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrations_Employees_Employeeid",
                table: "Administrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Employees_employee_id",
                table: "Drivers");

            migrationBuilder.DropTable(
                name: "roles_employees");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Administrations_Employeeid",
                table: "Administrations");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Workers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Drivers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "employee_id",
                table: "Drivers",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_employee_id",
                table: "Drivers",
                newName: "IX_Drivers_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Administrations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Employeeid",
                table: "Administrations",
                newName: "seat_number");

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bus_id",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "bus_id",
                table: "Administrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrations_employee_id",
                table: "Administrations",
                column: "employee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrations_Employees_employee_id",
                table: "Administrations",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Employees_EmployeeId",
                table: "Drivers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
