using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentModelNameInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Counselors_CounselorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments",
                newSchema: "EduGuide");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                schema: "EduGuide",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CounselorId",
                schema: "EduGuide",
                table: "Comments",
                newName: "IX_Comments_CounselorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                schema: "EduGuide",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Counselors_CounselorId",
                schema: "EduGuide",
                table: "Comments",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                schema: "EduGuide",
                table: "Comments",
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
                name: "FK_Comments_Counselors_CounselorId",
                schema: "EduGuide",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                schema: "EduGuide",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                schema: "EduGuide",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                schema: "EduGuide",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CounselorId",
                table: "Comment",
                newName: "IX_Comment_CounselorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Counselors_CounselorId",
                table: "Comment",
                column: "CounselorId",
                principalSchema: "EduGuide",
                principalTable: "Counselors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalSchema: "EduGuide",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
