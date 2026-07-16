using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class RateConfig : BaseEntityConfig<Rate>
    {
        public override void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rates", "EduGuide");

            builder.Property(x => x.Score)
                .IsRequired()
                .HasComment("امتیاز");

            builder.Property(x => x.RequestCounselorId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasComment("دانش‌آموزی که امتیاز داده");

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired(true);

            builder.HasOne(x => x.RequestCounselor)
                .WithMany()
                .HasForeignKey(x => x.RequestCounselorId)
                .IsRequired(true);
        }
    }
}