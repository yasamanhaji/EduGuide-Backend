using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserModelModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                comment: "آدرس ایمیل");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "نام");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "نام خانوادگی");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "هش پسوورد");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                comment: "رفرش توکن");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                schema: "EduGuide",
                table: "Users",
                type: "datetime2",
                nullable: true,
                comment: "انقضای رفرش توکن");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                comment: "نقش");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "نام کاربری");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "EduGuide",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "EduGuide",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
