using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactorstudenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Users_UserId",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students",
                newSchema: "EduGuide");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UserId",
                schema: "EduGuide",
                table: "Students",
                newName: "IX_Students_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                schema: "EduGuide",
                table: "Students",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                schema: "EduGuide",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                schema: "EduGuide",
                newName: "Student");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UserId",
                table: "Student",
                newName: "IX_Student_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Users_UserId",
                table: "Student",
                column: "UserId",
                principalSchema: "EduGuide",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
