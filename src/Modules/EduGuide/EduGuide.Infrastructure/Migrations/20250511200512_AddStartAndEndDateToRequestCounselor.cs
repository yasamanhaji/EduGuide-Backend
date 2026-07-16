using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStartAndEndDateToRequestCounselor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RequestCounselor_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestCounselor_Counselors_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestCounselor_Students_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestCounselor",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.RenameTable(
                name: "RequestCounselor",
                schema: "EduGuide",
                newName: "RequestCounselors",
                newSchema: "EduGuide");

            migrationBuilder.RenameIndex(
                name: "IX_RequestCounselor_StudentId",
                schema: "EduGuide",
                table: "RequestCounselors",
                newName: "IX_RequestCounselors_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestCounselor_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselors",
                newName: "IX_RequestCounselors_CounselorId");

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                comment: "تاریخ تولد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ تولد");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "EduGuide",
                table: "RequestCounselors",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ شروع");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "EduGuide",
                table: "RequestCounselors",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ شروع");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestCounselors",
                schema: "EduGuide",
                table: "RequestCounselors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RequestCounselors_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments",
                column: "RequestCounselorId",
                principalSchema: "EduGuide",
                principalTable: "RequestCounselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestCounselors_Counselors_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselors",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestCounselors_Students_StudentId",
                schema: "EduGuide",
                table: "RequestCounselors",
                column: "StudentId",
                principalSchema: "EduGuide",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RequestCounselors_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestCounselors_Counselors_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselors");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestCounselors_Students_StudentId",
                schema: "EduGuide",
                table: "RequestCounselors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestCounselors",
                schema: "EduGuide",
                table: "RequestCounselors");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "EduGuide",
                table: "RequestCounselors");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "EduGuide",
                table: "RequestCounselors");

            migrationBuilder.RenameTable(
                name: "RequestCounselors",
                schema: "EduGuide",
                newName: "RequestCounselor",
                newSchema: "EduGuide");

            migrationBuilder.RenameIndex(
                name: "IX_RequestCounselors_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor",
                newName: "IX_RequestCounselor_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestCounselors_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor",
                newName: "IX_RequestCounselor_CounselorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "EduGuide",
                table: "Students",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ تولد",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "تاریخ تولد");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestCounselor",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RequestCounselor_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments",
                column: "RequestCounselorId",
                principalSchema: "EduGuide",
                principalTable: "RequestCounselor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestCounselor_Counselors_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestCounselor_Students_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "StudentId",
                principalSchema: "EduGuide",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
