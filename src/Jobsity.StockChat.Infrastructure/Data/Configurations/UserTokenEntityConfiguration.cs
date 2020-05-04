using Jobsity.StockChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Infrastructure.Data.Configurations
{
    internal class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserTokenEntity>
    {
        public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
        {
            builder.HasKey(t => t.Token);
            builder.Property(t => t.Token).IsRequired().ValueGeneratedNever();

            builder.Property(t => t.Nickname).IsNickname();
            builder.Property(t => t.CreatedDate).IsRequired();
            builder.Property(t => t.ExpirationDate).IsRequired();

            builder.HasOne(t => t.User).WithMany(d => d.UserTokens).HasForeignKey(t => t.Nickname);

            builder.ToContainer("UserTokens");
        }
    }
}
