using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class CounselorRecruitmentConfig : BaseEntityConfig<CounselorRecruitment>
    {
        public override void Configure(EntityTypeBuilder<CounselorRecruitment> builder)
        {
            builder.ToTable("CounselorRecruitments", "EduGuide");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("نام");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("نام خانوادگی");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("آدرس ایمیل");

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11)
                .HasComment("شماره تماس");
            builder.Property(x => x.Province)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("استان");

            builder.Property(x => x.UniMajor)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("رشته دانشگاهی");

            builder.Property(x => x.HsMajor)
                .IsRequired()
                .HasComment("رشته تحصیلی");

            builder.Property(x => x.CountryRanking)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("رتبه کشوری");

            builder.Property(x => x.EntranceExamYear)
                .IsRequired()
                .HasMaxLength(4)
                .HasComment("سال کنکور");

            builder.Property(x => x.UniName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("نام دانشگاه");

            builder.Property(x => x.Employmenthistory)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("سوابق شغلی");

            builder.Property(x => x.SudentCardPicName)
                .HasComment("نام فایل عکس");
        }
    }
}