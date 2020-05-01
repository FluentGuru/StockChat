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
        public StockChatDbContext(DbContextOptions<StockChatDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ChatEntity> Chats { get; set; }

        public virtual DbSet<ChatParticipantEntity> ChatParticipants { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
