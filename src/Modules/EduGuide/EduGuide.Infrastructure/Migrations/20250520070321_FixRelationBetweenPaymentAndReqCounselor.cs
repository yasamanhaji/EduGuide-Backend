using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationBetweenPaymentAndReqCounselor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments",
                column: "RequestCounselorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RequestCounselorId",
                schema: "EduGuide",
                table: "Payments",
                column: "RequestCounselorId",
                unique: true);
        }
    }
}
