using System.Threading.Tasks;
using Dream.Business.TransferModels;

namespace Dream.Business.Abstract
{
    /// <summary>
    /// Crud methods for tasks.
    /// </summary>
    public interface ITaskEditorService
    {
        Task<(TaskDto task, string error)> AddTaskAsync(NewTaskDto task);
        Task<(TaskDto task, string error)> UpdateTaskAsync(int taskId, NewTaskDto task);
        Task<(TaskDto task, string error)> GetTaskAsync(int taskId);
        Task<(TaskDto task, string error)> DeleteTaskAsync(int taskId);
    }
}