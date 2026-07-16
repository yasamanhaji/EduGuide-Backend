using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStudentGradeLevelToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GradeLevel",
                schema: "EduGuide",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "پایه تحصیلی",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "پایه تحصیلی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GradeLevel",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "پایه تحصیلی",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "پایه تحصیلی");
        }
    }
}
