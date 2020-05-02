using Jobsity.StockChat.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Data.Configurations
{
    internal class ChatParticipantEntityConfiguration : IEntityTypeConfiguration<ChatParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<ChatParticipantEntity> builder)
        {
            builder.HasKey(t => new { t.Stock, t.Nickname });
            builder.Property(t => t.Stock).IsStock();
            builder.Property(t => t.Nickname).IsNickname();

            //builder.HasPartitionKey(t => t.Stock);

            builder.HasOne(t => t.Chat).WithMany(d => d.Participants).HasForeignKey(t => t.Stock);
            builder.HasOne(t => t.Participant).WithMany(d => d.Participations).HasForeignKey(t => t.Nickname);
        }
    }
}
