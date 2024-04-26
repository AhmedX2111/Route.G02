using Microsoft.EntityFrameworkCore.Migrations;

namespace Route.G02.DAL.Data.Migrations
{
    public partial class EmployeeDepartmentRelationship : Migration
    {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.AddColumn<int>(
                    name: "DepartmentId",
                    table: "Employees",
                    type: "int",
                    nullable: true);

                migrationBuilder.AddColumn<int>(
                    name: "DepartmentId1",
                    table: "Employees",
                    type: "int",
                    nullable: true);

                migrationBuilder.CreateIndex(
                    name: "IX_Employees_DepartmentId",
                    table: "Employees",
                    column: "DepartmentId");

                migrationBuilder.CreateIndex(
                    name: "IX_Employees_DepartmentId1",
                    table: "Employees",
                    column: "DepartmentId1");

                migrationBuilder.AddForeignKey(
                    name: "FK_Employees_Departments_DepartmentId",
                    table: "Employees",
                    column: "DepartmentId",
                    principalTable: "Departments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);

                migrationBuilder.AddForeignKey(
                    name: "FK_Employees_Departments_DepartmentId1",
                    table: "Employees",
                    column: "DepartmentId1",
                    principalTable: "Departments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropForeignKey(
                    name: "FK_Employees_Departments_DepartmentId",
                    table: "Employees");

                migrationBuilder.DropForeignKey(
                    name: "FK_Employees_Departments_DepartmentId1",
                    table: "Employees");

                migrationBuilder.DropIndex(
                    name: "IX_Employees_DepartmentId",
                    table: "Employees");

                migrationBuilder.DropIndex(
                    name: "IX_Employees_DepartmentId1",
                    table: "Employees");

                migrationBuilder.DropColumn(
                    name: "DepartmentId",
                    table: "Employees");

                migrationBuilder.DropColumn(
                    name: "DepartmentId1",
                    table: "Employees");
            }
}   }

