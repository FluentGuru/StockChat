using Jobsity.StockChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobsity.StockChat.Infrastructure.Data.Configurations
{
    internal class ChatMessageEntityConfiguration : IEntityTypeConfiguration<ChatMessageEntity>
    {
        public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();

            //builder.HasPartitionKey(t => t.Stock);

            builder.Property(t => t.Stock).IsStock();
            builder.Property(t => t.FromNickName).IsNickname();
            builder.Property(t => t.Message).IsRequired();
            builder.Property(t => t.SentTime).IsRequired();

            builder.HasOne(t => t.Sender).WithMany(d => d.Messages).HasForeignKey(t => t.FromNickName);
            builder.HasOne(t => t.Chat).WithMany(d => d.Messages).HasForeignKey(t => t.Stock);
        }
    }
}
