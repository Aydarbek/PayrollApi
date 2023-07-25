using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayrollApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    department_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payrolls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payrolls", x => x.id);
                    table.ForeignKey(
                        name: "FK_payrolls_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "Administration" },
                    { 2L, "Engineering" },
                    { 3L, "Finances" }
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "id", "department_id", "name" },
                values: new object[,]
                {
                    { 1L, 1L, "Bob" },
                    { 2L, 2L, "Kasun" },
                    { 3L, 2L, "Andrew" },
                    { 4L, 3L, "Molly" }
                });

            migrationBuilder.InsertData(
                table: "payrolls",
                columns: new[] { "id", "amount", "date", "employee_id", "status" },
                values: new object[,]
                {
                    { 1L, 20000f, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 0 },
                    { 2L, 30000f, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 1 },
                    { 3L, 15000f, new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 1 },
                    { 4L, 10000f, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_name",
                table: "departments",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_department_id",
                table: "employees",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_name",
                table: "employees",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payrolls_employee_id",
                table: "payrolls",
                column: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payrolls");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
