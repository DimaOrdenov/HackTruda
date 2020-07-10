using Microsoft.EntityFrameworkCore;
using HackTruda.API.Models;

namespace HackTruda.API.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Notification>().ToTable("Notification");
        }

        public DbSet<HackTruda.API.Models.Post> Post { get; set; }

        public DbSet<HackTruda.API.Models.Notification> Notification { get; set; }
    }
}