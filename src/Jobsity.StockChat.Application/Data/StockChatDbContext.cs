using Jobsity.StockChat.Application.Data.Configurations;
using Jobsity.StockChat.Application.Entities;
using Jobsity.StockChat.Domain.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Data
{
    public class StockChatDbContext : DbContext
    {
        public StockChatDbContext()
        {
        }

        public StockChatDbContext(DbContextOptions<StockChatDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ChatEntity> Chats { get; set; }

        public virtual DbSet<ChatParticipantEntity> ChatParticipants { get; set; }

        public virtual DbSet<ChatMessageEntity> ChatMessages { get; set; }

        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<UserTokenEntity> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatParticipantEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenEntityConfiguration());
        }

        //For migration purposes only
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "https://localhost:8081", 
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", 
                "StockChat");
        }
    }
}
