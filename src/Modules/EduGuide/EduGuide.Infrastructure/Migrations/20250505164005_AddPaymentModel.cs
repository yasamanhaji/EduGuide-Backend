using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "مبلغ قابل پرداخت"),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false, comment: "پرداخت شده یا خیر"),
                    CounselingDuration = table.Column<int>(type: "int", nullable: false, comment: "مدت مشاوره"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    CounselorId = table.Column<long>(type: "bigint", nullable: false),
                    RequestCounselorId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Counselors_CounselorId",
                        column: x => x.CounselorId,
                        principalSchema: "EduGuide",
                        principalTable: "Counselors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_RequestCounselor_RequestCounselorId",
                        column: x => x.RequestCounselorId,
                        principalSchema: "EduGuide",
                        principalTable: "RequestCounselor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "EduGuide",
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CounselorId",
                table: "Payment",
                column: "CounselorId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RequestCounselorId",
                table: "Payment",
                column: "RequestCounselorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_StudentId",
                table: "Payment",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
