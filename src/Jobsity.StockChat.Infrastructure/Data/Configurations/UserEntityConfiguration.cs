﻿using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Infrastructure.Data.Configurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(t => t.Nickname);
            builder.Property(t => t.Nickname).IsNickname().ValueGeneratedNever();

            //builder.HasPartitionKey(t => t.CreatedDate.Year);

            builder.Property(t => t.CreatedDate).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.LastLoginDate).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.PasswordHash).IsRequired();
            builder.Property(t => t.PasswordSalt).IsRequired().HasMaxLength(UserAuthConstants.PasswordSaltLength);

            builder.HasMany(t => t.Participations).WithOne(d => d.Participant).HasForeignKey(d => d.Nickname);
            builder.HasMany(t => t.Messages).WithOne(d => d.Sender).HasForeignKey(d => d.FromNickName);

            builder.ToContainer("Users");
        }
    }
}
