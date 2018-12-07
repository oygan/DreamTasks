using System.Threading.Tasks;
using Dream.Business.TransferModels;

namespace Dream.Business.Abstract
{
    /// <summary>
    /// Task searching.
    /// </summary>
    public interface ITaskSearchService
    {
        Task<TaskListPageDto> GetTasksAsync(TaskSearchFilterDto filter);
    }
}