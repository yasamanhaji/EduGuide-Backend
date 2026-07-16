using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationBetweenStudentAndCounselorAndRequestCounselor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RequestCounselor_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.DropIndex(
                name: "IX_RequestCounselor_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCounselor_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "CounselorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCounselor_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RequestCounselor_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.DropIndex(
                name: "IX_RequestCounselor_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCounselor_CounselorId",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "CounselorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestCounselor_StudentId",
                schema: "EduGuide",
                table: "RequestCounselor",
                column: "StudentId",
                unique: true);
        }
    }
}
