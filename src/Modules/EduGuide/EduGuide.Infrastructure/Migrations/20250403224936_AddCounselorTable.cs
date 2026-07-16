using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCounselorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counselors",
                schema: "EduGuide",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "کدملی"),
                    CardNum = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "شماره کارت"),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "درباره من"),
                    CounselorRecruitmentId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counselors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counselors_CounselorRecruitments_CounselorRecruitmentId",
                        column: x => x.CounselorRecruitmentId,
                        principalSchema: "EduGuide",
                        principalTable: "CounselorRecruitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Counselors_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "EduGuide",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counselors_CounselorRecruitmentId",
                schema: "EduGuide",
                table: "Counselors",
                column: "CounselorRecruitmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Counselors_UserId",
                schema: "EduGuide",
                table: "Counselors",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counselors",
                schema: "EduGuide");
        }
    }
}
