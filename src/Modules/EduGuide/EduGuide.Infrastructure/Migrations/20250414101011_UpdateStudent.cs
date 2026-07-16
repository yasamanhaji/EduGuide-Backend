using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                comment: "درباره من");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "EduGuide",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "تاریخ تولد");

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileComplete",
                schema: "EduGuide",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentPhoneNumber",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                comment: "شماره تلفن والد ");

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                comment: "نام مدرسه");

            migrationBuilder.AddColumn<string>(
                name: "StudentPhoneNumber",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                comment: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsProfileComplete",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentPhoneNumber",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolName",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentPhoneNumber",
                schema: "EduGuide",
                table: "Students");
        }
    }
}
