using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPaymentNameInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Counselors_CounselorId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_RequestCounselor_RequestCounselorId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Students_StudentId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments",
                newSchema: "EduGuide");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_StudentId",
                schema: "EduGuide",
                table: "Payments",
                newName: "IX_Payments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments",
                newName: "IX_Payments_RequestCounselorId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_CounselorId",
                schema: "EduGuide",
                table: "Payments",
                newName: "IX_Payments_CounselorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                schema: "EduGuide",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Counselors_CounselorId",
                schema: "EduGuide",
                table: "Payments",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Payments_Students_StudentId",
                schema: "EduGuide",
                table: "Payments",
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
                name: "FK_Payments_Counselors_CounselorId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RequestCounselor_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_StudentId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                schema: "EduGuide",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_StudentId",
                table: "Payment",
                newName: "IX_Payment_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_RequestCounselorId",
                table: "Payment",
                newName: "IX_Payment_RequestCounselorId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CounselorId",
                table: "Payment",
                newName: "IX_Payment_CounselorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Counselors_CounselorId",
                table: "Payment",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_RequestCounselor_RequestCounselorId",
                table: "Payment",
                column: "RequestCounselorId",
                principalSchema: "EduGuide",
                principalTable: "RequestCounselor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Students_StudentId",
                table: "Payment",
                column: "StudentId",
                principalSchema: "EduGuide",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
