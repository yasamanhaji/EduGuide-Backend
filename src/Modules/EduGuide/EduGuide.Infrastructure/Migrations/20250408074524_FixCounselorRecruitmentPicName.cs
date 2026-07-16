using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCounselorRecruitmentPicName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SudentCardPicUrl",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.DropColumn(
                name: "UrlExpiryTime",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.AddColumn<string>(
                name: "SudentCardPicName",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(max)",
                nullable: true,
                comment: "نام فایل عکس");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SudentCardPicName",
                schema: "EduGuide",
                table: "CounselorRecruitments");

            migrationBuilder.AddColumn<string>(
                name: "SudentCardPicUrl",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "nvarchar(max)",
                nullable: true,
                comment: "لینک دانلود عکس");

            migrationBuilder.AddColumn<DateTime>(
                name: "UrlExpiryTime",
                schema: "EduGuide",
                table: "CounselorRecruitments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "زمان منقضی شدن لینک");
        }
    }
}
