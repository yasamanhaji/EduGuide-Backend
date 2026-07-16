using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Counselors_CounselorId",
                schema: "EduGuide",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "CounselorId",
                schema: "EduGuide",
                table: "Rates",
                newName: "RequestCounselorId");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_CounselorId",
                schema: "EduGuide",
                table: "Rates",
                newName: "IX_Rates_RequestCounselorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_RequestCounselors_RequestCounselorId",
                schema: "EduGuide",
                table: "Rates",
                column: "RequestCounselorId",
                principalSchema: "EduGuide",
                principalTable: "RequestCounselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_RequestCounselors_RequestCounselorId",
                schema: "EduGuide",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "RequestCounselorId",
                schema: "EduGuide",
                table: "Rates",
                newName: "CounselorId");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_RequestCounselorId",
                schema: "EduGuide",
                table: "Rates",
                newName: "IX_Rates_CounselorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Counselors_CounselorId",
                schema: "EduGuide",
                table: "Rates",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
