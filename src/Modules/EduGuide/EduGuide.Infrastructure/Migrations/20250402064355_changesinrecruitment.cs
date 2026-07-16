using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduGuide.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changesinrecruitment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CounselorRecruitments",
                schema: "EduGuide",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "نام"),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "نام خانوادگی"),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "آدرس ایمیل"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false, comment: "شماره تماس"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "شهر"),
                    Major = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "رشته تحصیلی"),
                    CountryRanking = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "رتبه کشوری"),
                    Sahmie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "سهمیه"),
                    EntranceExamYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "سال کنکور"),
                    EntranceYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "سال ورود به دانشگاه"),
                    UniName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "نام دانشگاه"),
                    Employmenthistory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "سوابق شغلی"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounselorRecruitments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounselorRecruitments",
                schema: "EduGuide");
        }
    }
}
