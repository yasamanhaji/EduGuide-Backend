using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "متن کامنت"),
                    Approved = table.Column<bool>(type: "bit", nullable: false, comment: "تایید شده توسط ادمین"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "آیدی کاربر(دانش‌آموز)"),
                    CounselorId = table.Column<long>(type: "bigint", nullable: false, comment: "آیدی ‌مشاور"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Counselors_CounselorId",
                        column: x => x.CounselorId,
                        principalSchema: "EduGuide",
                        principalTable: "Counselors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "EduGuide",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CounselorId",
                table: "Comment",
                column: "CounselorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
