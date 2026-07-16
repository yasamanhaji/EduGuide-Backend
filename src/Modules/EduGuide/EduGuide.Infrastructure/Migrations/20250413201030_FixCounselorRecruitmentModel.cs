using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCounselorRecruitmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntranceYear",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.DropColumn(
                name: "Major",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.AddColumn<int>(
                name: "HsMajor",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "رشته تحصیلی");

            migrationBuilder.AddColumn<string>(
                name: "UniMajor",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "رشته دانشگاهی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HsMajor",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.DropColumn(
                name: "UniMajor",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.AddColumn<string>(
                name: "EntranceYear",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "",
                comment: "سال ورود به دانشگاه");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "رشته تحصیلی");
        }
    }
}
