using System;
using Dream.DataAccess;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Dream.UnitTests.Common
{
    /// <summary>
    /// Context Factory for Tests
    /// </summary>
    class TestContextFactory
    {
        /// <summary>
        ///  Create context with test database.
        /// </summary>
        public static DreamContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<DreamContext>()
                .UseMySql("Server=localhost;Database=dreamTest;User=root;Password=root;",
                    mySqlOptions =>
                    {
                        // replace with your Server Version and Type
                        mySqlOptions.ServerVersion(new Version(8, 0, 13), ServerType.MySql);
                    }
                )
                .Options;

            return new DreamContext(options);
        }
    }
}