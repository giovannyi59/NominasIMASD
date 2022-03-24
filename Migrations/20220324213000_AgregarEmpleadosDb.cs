using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomina2022.Migrations
{
    public partial class AgregarEmpleadosDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Salario",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salario",
                table: "Empleados");
        }
    }
}
