using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class CounselorConfig : BaseEntityConfig<Counselor>
    {
        public override void Configure(EntityTypeBuilder<Counselor> builder)
        {
            builder.ToTable("Counselors", "EduGuide");

            builder.Property(x => x.NationalCode)
                .HasComment("کدملی");

            builder.Property(x => x.CardNum)
                .HasComment("شماره کارت");

            builder.Property(x => x.AboutMe)
                .HasComment("درباره من");

            builder.HasOne(x => x.CounselorRecruitment)
                .WithOne()
                .HasForeignKey<Counselor>(x => x.CounselorRecruitmentId);

            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Counselor>(x => x.UserId);
        }
    }
}