using System.Linq;
using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Extensions;
using Dream.Business.TransferModels;
using Dream.DataAccess;
using Dream.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dream.Business.Services
{
    /// <summary>
    /// Task searching implementation.
    /// </summary>
    public class TaskSearchService : ITaskSearchService
    {
        private readonly DreamContext _dbContext;

        public TaskSearchService(DreamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskListPageDto> GetTasksAsync(TaskSearchFilterDto filter)
        {
            IQueryable<WorkTask> tasks = _dbContext.Set<WorkTask>();

            // filter
            tasks = AcceptFilter(filter, tasks);

            // order
            tasks = AcceptOrder(filter, tasks);

            // paging
            tasks = tasks.Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize);

            // result
            var filteredTasks = await tasks.ToListAsync();
            TaskListPageDto page = new TaskListPageDto() {Tasks = filteredTasks.Select(t => t.AsTask()).ToList()};
            return page;
        }

        private static IQueryable<WorkTask> AcceptOrder(TaskSearchFilterDto filter, IQueryable<WorkTask> tasks)
        {
            switch (filter.Order)
            {
                case OrderTaskEnum.None:
                    break;
                case OrderTaskEnum.Priority:
                    tasks = tasks.OrderBy(t => t.Priority);
                    break;
                case OrderTaskEnum.CreateDate:
                    tasks = tasks.OrderBy(t => t.CreateDate);
                    break;
            }

            return tasks;
        }

        private IQueryable<WorkTask> AcceptFilter(TaskSearchFilterDto filter, IQueryable<WorkTask> tasks)
        {
            if (filter.ProjectId != null)
                tasks = tasks.Where(t => t.ProjectId == filter.ProjectId.Value);
            if (filter.CreateDateMin != null)
                tasks = tasks.Where(t => t.CreateDate >= filter.CreateDateMin.Value);
            if (filter.CreateDateMax != null)
                tasks = tasks.Where(t => t.CreateDate <= filter.CreateDateMax.Value);
            if (filter.Priority != null)
                tasks = tasks.Where(t => t.Priority == filter.Priority.Value);
            if (filter.FlowStatus != null)
                tasks = tasks.Where(t => t.FlowStatus == filter.FlowStatus.Value);
            return tasks;
        }
    }
}