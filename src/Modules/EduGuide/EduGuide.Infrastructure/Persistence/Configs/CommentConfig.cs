using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class CommentConfig : BaseEntityConfig<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments", "EduGuide");

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("متن کامنت");  
            
            builder.Property(x => x.Approved)
                .HasComment("تایید شده توسط ادمین");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasComment("آیدی کاربر(دانش‌آموز)");

            builder.Property(x => x.CounselorId)
                .IsRequired()
                .HasComment("آیدی ‌مشاور");

            builder.HasOne(x => x.Counselor)
                .WithMany()
                .HasForeignKey(x => x.CounselorId);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}