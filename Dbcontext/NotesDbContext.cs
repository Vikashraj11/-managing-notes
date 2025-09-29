using Managingnotes.Models;
using Microsoft.EntityFrameworkCore;

namespace Managingnotes.Dbcontext
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        public DbSet<Notes> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notes>().Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }

    }
}
