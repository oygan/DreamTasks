using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Dream.DataAccess
{
    /// <summary>
    /// Context factory for package manager console only. Need for any migrate generation.
    /// </summary>
    public class DreamContextFactory : IDesignTimeDbContextFactory<DreamContext>
    {
        /// <summary>
        /// Create context for package manager console.
        /// Use Microsoft.EntityFrameworkCore.Design package.
        /// </summary>
        public DreamContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<DreamContext>()
                .UseMySql("Server=localhost;Database=dream;User=root;Password=root;",
                    mySqlOptions =>
                    {
                        // replace with your Server Version and Type
                        mySqlOptions.ServerVersion(new Version(8, 0, 13), ServerType.MySql);
                    }
                )
                .Options;

            var context = new DreamContext(options);
            return context;
        }
    }
}