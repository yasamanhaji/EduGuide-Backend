using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCounselorRecruitmentProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.DropColumn(
                name: "Sahmie",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "استان");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "شهر");

            migrationBuilder.AddColumn<string>(
                name: "Sahmie",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "سهمیه");
        }
    }
}
