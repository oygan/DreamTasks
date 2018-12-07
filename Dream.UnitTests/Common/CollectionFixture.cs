using System;
using Dream.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Dream.UnitTests.Common
{
    /// <summary>
    /// Test initialization
    /// </summary>
    public class CollectionFixture : IDisposable
    {
        /// <summary>
        /// Do "global" initialization here; Only called once.
        /// </summary>
        public CollectionFixture()
        {
            RecreateDatabase();
        }

        /// <summary>
        /// Do "global" teardown here; Only called once.
        /// </summary>
        public void Dispose()
        {
             
        }

        /// <summary>
        /// Init test database
        /// </summary>
        private void RecreateDatabase()
        {
            DreamContext context = TestContextFactory.CreateTestContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}