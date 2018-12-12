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
    /// Task id validation filter.
    /// </summary>
    public class TaskIdValidator : IAsyncActionFilter
    {
        private readonly DreamContext _dbContext;

        public TaskIdValidator(DreamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var taskId = context.ActionArguments.Extract<int>();
            var isExist = await _dbContext.Set<WorkTask>().Where(x => x.Id == taskId).AnyAsync();
            if (!isExist)
            {
                context.Result = new NotFoundObjectResult(new { Error = $"Task not found, unknown id: {taskId}" });
                return;
            };

            await next();
        }
    }
}