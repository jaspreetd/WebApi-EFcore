using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "EmployeeSequence");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [EmployeeSequence]"),
                    EmpName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    EmpCity = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    EmpDept = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpID);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpID", "EmpCity", "EmpDept", "EmpName" },
                values: new object[] { 1, "Hartford", "BI Architecture", "Jaspreet" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropSequence(
                name: "EmployeeSequence");
        }
    }
}
