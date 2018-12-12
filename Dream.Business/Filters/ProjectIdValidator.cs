using System.Linq;
using System.Threading.Tasks;
using Dream.Business.Extensions;
using Dream.DataAccess;
using Dream.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Dream.Business.Filters
{
    /// <summary>
    /// Project id validation filter.
    /// </summary>
    public class ProjectIdValidator : IAsyncActionFilter
    {
        private readonly DreamContext _dbContext;

        public ProjectIdValidator(DreamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var projectId = context.ActionArguments.Extract<int>();
            var isExist = await _dbContext.Set<Project>().Where(x => x.Id == projectId).AnyAsync();
            if (!isExist)
            {
                context.Result = new NotFoundObjectResult(new { Error = $"Project not found, unknown id: {projectId}" });
                return;
            };

            await next();
        }
    }
}