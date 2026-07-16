using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class UserConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "EduGuide");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("نام");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("نام خانوادگی");

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasComment("نام کاربری");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("آدرس ایمیل");

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasComment("هش پسوورد");

            builder.Property(x => x.Role)
                .HasComment("نقش");

            builder.Property(x => x.RefreshToken)
                .HasComment("رفرش توکن");

            builder.Property(x => x.RefreshTokenExpiryTime)
                .HasComment("انقضای رفرش توکن");

            //builder.HasIndex(x => x.Email)
            //    .IsUnique()
            //    .HasFilter("[Email] IS NOT NULL");
        }
    }
}