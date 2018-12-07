using Dream.Business.Abstract;
using Dream.Business.Filters;
using Dream.Business.Services;
using Dream.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dream.WebApi
{
    /// <summary>
    /// Configuration.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.UseMySql();

            services.AddScoped<DreamContext, DreamContext>();
            services.AddScoped<IProjectEditorService, ProjectEditorService>();
            services.AddScoped<ITaskEditorService, TaskEditorService>();
            services.AddScoped<ITaskSearchService, TaskSearchService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
           
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DreamContext>();
                context.ApplyMigration();
            }
        }
    }
}
