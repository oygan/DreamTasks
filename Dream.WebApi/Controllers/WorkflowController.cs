using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Extensions;
using Dream.Business.Filters;
using Dream.Business.TransferModels;
using Microsoft.AspNetCore.Mvc;

namespace Dream.WebApi.Controllers
{
    /// <summary>
    ///  Workflow methods for task.
    /// </summary>
    [Route("api/[controller]")]
    public class WorkflowController : Controller
    {
        private readonly IWorkflowService _workflowService;

        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [HttpPost("start/{taskId}")]
        [TypeFilter(typeof(TaskIdValidator))]
        public async Task<TaskDto> StartTask(int taskId)
        {
            var result = await _workflowService.StartTaskAsync(taskId);
            return result.task.CheckError(Response, result.error);
        }

        [HttpPost("reopen/{taskId}")]
        [TypeFilter(typeof(TaskIdValidator))]
        public async Task<TaskDto> ReopenTask(int taskId)
        {
            var result = await _workflowService.ReopenTaskAsync(taskId);
            return result.task.CheckError(Response, result.error);
        }

        [HttpPost("close/{taskId}")]
        [TypeFilter(typeof(TaskIdValidator))]
        public async Task<TaskDto> CloseTask(int taskId)
        {
            var result = await _workflowService.CloseTaskAsync(taskId);
            return result.task.CheckError(Response, result.error);
        }
    }
}