using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityModel.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndPasswordToStudentAndInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Instructor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Instructor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Password" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Password" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Password" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Password" },
                values: new object[] { "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Instructor");
        }
    }
}
