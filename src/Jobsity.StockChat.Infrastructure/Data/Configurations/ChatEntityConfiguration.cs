﻿using Jobsity.StockChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Infrastructure.Data.Configurations
{
    internal class ChatEntityConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.HasKey(t => t.Stock);
            builder.Property(t => t.Stock).IsStock().ValueGeneratedNever();
            builder.Property(t => t.OwnerNickname).IsNickname();

            //builder.HasPartitionKey(t => t.Stock);

            builder.HasOne(t => t.Owner).WithMany().HasForeignKey(t => t.OwnerNickname);
            builder.HasMany(t => t.Messages).WithOne(d => d.Chat).HasForeignKey(d => d.Stock);
            builder.HasMany(t => t.Participants).WithOne(d => d.Chat).HasForeignKey(d => d.Stock);

            builder.ToContainer("Chats");

        }
    }
}
