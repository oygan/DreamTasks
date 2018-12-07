using Dream.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dream.DataAccess
{
    /// <summary>
    /// Dream database context.
    /// </summary>
    public class DreamContext : DbContext
    {
        public DreamContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // == project 
            modelBuilder.Entity<Project>().HasKey(c => c.Id);
            modelBuilder.Entity<Project>().HasMany(x => x.Tasks).WithOne(x => x.Project).HasForeignKey(x => x.ProjectId);

            // == task
            modelBuilder.Entity<WorkTask>().HasKey(c => c.Id);
        }

        public void ApplyMigration()
        {
            Database.Migrate();
        }
    }
}
