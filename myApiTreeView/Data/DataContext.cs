using Microsoft.EntityFrameworkCore;
using myApiTreeView.Models;

namespace myApiTreeView.API.Data
{
    public class DataContext : DbContext
    {    

        public DbSet<Folder> folders { get; set; }

        public DbSet<TestCase> testCases { get; set; }
        public DataContext(DbContextOptions<DataContext>  options) : base (options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Folder>()
                .HasMany(p => p.SubFolders)
                .WithOne(p => p.ParentFolder)
                .HasForeignKey(p => p.ParentFolderId);
        }
    }   
}