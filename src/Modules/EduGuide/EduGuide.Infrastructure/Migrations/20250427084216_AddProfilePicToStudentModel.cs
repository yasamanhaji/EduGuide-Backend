using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePicToStudentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicName",
                schema: "EduGuide",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "نام عکس پروفایل دانش آموز");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicName",
                schema: "EduGuide",
                table: "Students");
        }
    }
}
