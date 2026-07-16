using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "RequestCounselor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Counselors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "Counselors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EduGuide",
                table: "CounselorRecruitments");
        }
    }
}
