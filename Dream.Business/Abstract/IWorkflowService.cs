using System.Threading.Tasks;
using Dream.Business.TransferModels;

namespace Dream.Business.Abstract
{
    /// <summary>
    /// Workflow methods for task.
    /// </summary>
    public interface IWorkflowService
    {
        Task<(TaskDto task, string error)> StartTaskAsync(int taskId);
        Task<(TaskDto task, string error)> ReopenTaskAsync(int taskId);
        Task<(TaskDto task, string error)> CloseTaskAsync(int taskId);
    }
}