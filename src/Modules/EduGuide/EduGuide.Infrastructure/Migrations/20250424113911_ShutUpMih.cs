using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShutUpMih : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counselors_CounselorRecruitments_CounselorRecruitmentId",
                schema: "EduGuide",
                table: "Counselors");

            migrationBuilder.DropForeignKey(
                name: "FK_Counselors_Users_UserId",
                schema: "EduGuide",
                table: "Counselors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Counselors_CounselorRecruitments_CounselorRecruitmentId",
                schema: "EduGuide",
                table: "Counselors",
                column: "CounselorRecruitmentId",
                principalSchema: "EduGuide",
                principalTable: "CounselorRecruitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Counselors_Users_UserId",
                schema: "EduGuide",
                table: "Counselors",
                column: "UserId",
                principalSchema: "EduGuide",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                schema: "EduGuide",
                table: "Students",
                column: "UserId",
                principalSchema: "EduGuide",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counselors_CounselorRecruitments_CounselorRecruitmentId",
                schema: "EduGuide",
                table: "Counselors");

            migrationBuilder.DropForeignKey(
                name: "FK_Counselors_Users_UserId",
                schema: "EduGuide",
                table: "Counselors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Counselors_CounselorRecruitments_CounselorRecruitmentId",
                schema: "EduGuide",
                table: "Counselors",
                column: "CounselorRecruitmentId",
                principalSchema: "EduGuide",
                principalTable: "CounselorRecruitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Counselors_Users_UserId",
                schema: "EduGuide",
                table: "Counselors",
                column: "UserId",
                principalSchema: "EduGuide",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                schema: "EduGuide",
                table: "Students",
                column: "UserId",
                principalSchema: "EduGuide",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
