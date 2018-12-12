using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Extensions;
using Dream.Business.Filters;
using Dream.Business.TransferModels;
using Microsoft.AspNetCore.Mvc;

namespace Dream.WebApi.Controllers
{
    /// <summary>
    /// This controller contains methods to create, read, update and delete tasks.
    /// </summary>
    [Route("api/[controller]")]
    [TypeFilter(typeof(InputStateValidator))]
    public class TasksController : Controller
    {
        private readonly ITaskEditorService _editorService;

        public TasksController(ITaskEditorService editorService)
        {
            _editorService = editorService;
        }

        [HttpGet("{taskId}")]
        [TypeFilter(typeof(TaskIdValidator))]
        public async Task<TaskDto> GetTask(int taskId)
        {
            var result = await _editorService.GetTaskAsync(taskId);
            return result.task.CheckError(Response, result.error);
        }

        [HttpPost()]
        public async Task<TaskDto> PostTask([FromBody] NewTaskDto task)
        {
            var result = await _editorService.AddTaskAsync(task);
            return result.task.CheckError(Response, result.error);
        }

        [HttpPut("{taskId}")]
        [TypeFilter(typeof(TaskIdValidator))]
        public async Task<TaskDto> PutTask(int taskId, [FromBody] NewTaskDto task)
        {
            var result = await _editorService.UpdateTaskAsync(taskId, task);
            return result.task.CheckError(Response, result.error);
        }

        [HttpDelete("{taskId}")]
        [TypeFilter(typeof(TaskIdValidator))]
        public async Task<TaskDto> DeleteTask(int taskId)
        {
            var result = await _editorService.DeleteTaskAsync(taskId);
            return result.task.CheckError(Response, result.error);
        }
    }
}