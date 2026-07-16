using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestCounselor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestCounselor",
                schema: "EduGuide",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "رشته تحصیلی"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    CounselorId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCounselor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCounselor_Counselors_CounselorId",
                        column: x => x.CounselorId,
                        principalSchema: "EduGuide",
                        principalTable: "Counselors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestCounselor_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "EduGuide",
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestCounselor",
                schema: "EduGuide");
        }
    }
}
