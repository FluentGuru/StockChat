using Jobsity.StockChat.Infrastructure.Data.Configurations;
using Jobsity.StockChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobsity.StockChat.Infrastructure.Data
{
    public class StockChatDbContext : DbContext
    {
        public StockChatDbContext(DbContextOptions<StockChatDbContext> options) : base(options)
        {
            Database.EnsureCreated();
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
    }
}
