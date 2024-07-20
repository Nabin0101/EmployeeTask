using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeeTask.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeCode = table.Column<string>(type: "text", nullable: false),
                    IsDisable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_employee_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeJobHistories",
                columns: table => new
                {
                    EmployeeJobHistoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeJobHistories", x => x.EmployeeJobHistoryId);
                    table.ForeignKey(
                        name: "FK_employeeJobHistories_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeJobHistories_positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeePositions",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeePositions", x => new { x.EmployeeId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_employeePositions_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeePositions_positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_PersonId",
                table: "employee",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employeeJobHistories_EmployeeId",
                table: "employeeJobHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeJobHistories_PositionId",
                table: "employeeJobHistories",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_employeePositions_PositionId",
                table: "employeePositions",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeJobHistories");

            migrationBuilder.DropTable(
                name: "employeePositions");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "people");
        }
    }
}
