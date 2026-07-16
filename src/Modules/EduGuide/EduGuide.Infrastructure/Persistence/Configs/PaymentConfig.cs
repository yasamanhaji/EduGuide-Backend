using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class PaymentConfig : BaseEntityConfig<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "EduGuide");

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("مبلغ قابل پرداخت");

            builder.Property(x => x.IsPaid)
                .IsRequired()
                .HasComment("پرداخت شده یا خیر");

            builder.Property(x => x.CounselingDuration)
                .IsRequired()
                .HasComment("مدت مشاوره");

            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId);

            builder.HasOne(x => x.Counselor)
                .WithMany()
                .HasForeignKey(x => x.CounselorId);

            builder.HasOne(x => x.RequestCounselor)
                .WithMany()
                .HasForeignKey(x => x.RequestCounselorId);
        }
    }
}
