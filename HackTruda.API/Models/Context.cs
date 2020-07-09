using Microsoft.EntityFrameworkCore;

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
        }

        public DbSet<HackTruda.API.Models.Post> Post { get; set; }
    }
}