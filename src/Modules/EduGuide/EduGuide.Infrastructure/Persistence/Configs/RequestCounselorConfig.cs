using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class RequestCounselorConfig : BaseEntityConfig<RequestCounselor>
    {
        public override void Configure(EntityTypeBuilder<RequestCounselor> builder)
        {
            builder.ToTable("RequestCounselors", "EduGuide");

            builder.Property(x =>x.status)
               .IsRequired()
               .HasComment("رشته تحصیلی");

            builder.Property(x => x.StartDate)
                .HasComment("تاریخ شروع");

            builder.Property(x => x.EndDate)
                .HasComment("تاریخ شروع");

            builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId); 

            builder.HasOne(x => x.Counselor)
            .WithMany()
            .HasForeignKey(x => x.CounselorId); 

        }
    }
}