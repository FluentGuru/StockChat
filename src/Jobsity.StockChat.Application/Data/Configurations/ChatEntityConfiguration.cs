using Jobsity.StockChat.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Data.Configurations
{
    internal class ChatEntityConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.HasKey(t => t.Stock);
            builder.Property(t => t.Stock).IsStock();
            builder.Property(t => t.OwnerNickname).IsNickname();

            //builder.HasPartitionKey(t => t.Stock);

            builder.HasOne(t => t.Owner).WithMany().HasForeignKey(t => t.OwnerNickname);
            builder.HasMany(t => t.Messages).WithOne(d => d.Chat).HasForeignKey(d => d.Stock);
            builder.HasMany(t => t.Participants).WithOne(d => d.Chat).HasForeignKey(d => d.Stock);

        }
    }
}
