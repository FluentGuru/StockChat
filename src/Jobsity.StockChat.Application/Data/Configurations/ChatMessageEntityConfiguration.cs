using Jobsity.StockChat.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Data.Configurations
{
    internal class ChatMessageEntityConfiguration : IEntityTypeConfiguration<ChatMessageEntity>
    {
        public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(t => t.Stock).IsStock();
            builder.Property(t => t.FromNickName).IsNickname();
            builder.Property(t => t.Message).IsRequired();
            builder.Property(t => t.Type).IsRequired();

            builder.HasOne(t => t.Sender).WithMany(d => d.Messages).HasForeignKey(t => t.FromNickName);
            builder.HasOne(t => t.Chat).WithMany(d => d.Messages).HasForeignKey(t => t.Stock);
        }
    }
}
