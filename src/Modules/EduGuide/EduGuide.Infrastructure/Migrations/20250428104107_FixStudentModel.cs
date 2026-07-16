using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Major",
                schema: "EduGuide",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "رشته تحصیلی",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true,
                oldComment: "رشته تحصیلی");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "استان");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Province",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Major",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                comment: "رشته تحصیلی",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "رشته تحصیلی");
        }
    }
}
