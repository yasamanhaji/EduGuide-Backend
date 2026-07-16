using Base.Infrastructure.Persistence.Configs;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduGuide.Infrastructure.Persistence.Configs
{
    public class MessageConfig : BaseEntityConfig<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages", "ChatHub");

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(1000)
                .HasComment("متن پیام");

            builder.Property(x => x.Seen)
                .HasComment("مشاهده شده!");

            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .IsRequired(true);

            builder.HasOne(x => x.Receiver)
                .WithMany()
                .HasForeignKey(x => x.ReceiverId)
                .IsRequired(true);
        }
    }
}
