using System;
using Dream.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Dream.WebApi
{
    /// <summary>
    /// Extensions for startup
    /// </summary>
    public static class StartupExtension
    {
        /// <summary>
        /// Configure services to connect MySql.
        /// </summary>
        public static void UseMySql(this IServiceCollection services)
        {
            services.AddDbContextPool<DreamContext>(
                // replace with your Connection String
                options => options.UseMySql("Server=localhost;Database=dream;User=root;Password=root;",
                    mySqlOptions =>
                    {
                        // replace with your Server Version and Type
                        mySqlOptions.ServerVersion(new Version(8, 0, 13), ServerType.MySql);
                    }
                ));
        }
    }
}