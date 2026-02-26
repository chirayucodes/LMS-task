using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryMinimalAPI.Persistence
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BookDetails> BookDetails { get; init; }
        public DbSet<Categories> Categories { get; init; }
        public DbSet<Members> Members { get; init; } 
        public DbSet<MemberType> MemberType { get; init; }
        public DbSet<BookIssueDetails> BookIssueDetails { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Type t = typeof(AppDbContext);
            modelBuilder.ApplyConfigurationsFromAssembly(t.Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
