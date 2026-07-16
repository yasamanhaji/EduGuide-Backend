using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class StudentConfig : BaseEntityConfig<Student>
    {

        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students", "EduGuide");

            builder.Property(x => x.GradeLevel)
                .HasComment("پایه تحصیلی");

            builder.Property(x => x.LastGradeGPA)
                .HasComment("معدل پایه قبلی");

            builder.Property(x => x.Major)
              .HasComment("رشته تحصیلی");

            builder.Property(x => x.AboutMe)
                .HasComment("درباره من");

            builder.Property(x => x.BirthDate)
                .HasComment("تاریخ تولد");

            builder.Property(x => x.SchoolName)
                .HasComment("نام مدرسه");


            builder.Property(x => x.StudentPhoneNumber)
                .HasComment("شماره تلفن دانش اموز");


            builder.Property(x => x.ParentPhoneNumber)
                .HasComment("شماره تلفن والد ");

            builder.Property(x => x.StudentPhoneNumber)
               .HasComment("");

            builder.Property(x => x.PicName)
                .HasMaxLength(100)
                .HasComment("نام عکس پروفایل دانش آموز");

            builder.HasOne(x => x.User)
               .WithOne()
               .HasForeignKey<Student>(x => x.UserId);

            builder.Property(x => x.Province)
                .HasMaxLength(50)
                .HasComment("استان");
        }
    }
}