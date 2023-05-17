using ItunesApi.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ItunesApi.Infrastructure.Context
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Logins { get; set; }
        public virtual DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {

                entity.HasKey(c => c.Id);

                entity.Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(255);
                modelBuilder.Entity<Account>().HasIndex(u => u.UserName).IsUnique();

                entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);
                modelBuilder.Entity<Account>().HasIndex(u => u.Email).IsUnique();

                entity.HasMany(entity => entity.ScoreList);
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Points)
                .HasMaxLength(255);

                entity.Property(c => c.Date)
                .HasMaxLength(255);

                entity.HasOne(entity => entity.Account);


            }) ;
        }

    }
}
