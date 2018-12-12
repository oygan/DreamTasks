using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Filters;
using Dream.Business.TransferModels;
using Microsoft.AspNetCore.Mvc;

namespace Dream.WebApi.Controllers
{
    /// <summary>
    /// Search methods for tasks.
    /// </summary>
    [Route("api/[controller]")]
    [TypeFilter(typeof(ModelStateValidator))]
    public class SearchTasksController : Controller
    {
        private readonly ITaskSearchService _searchService;

        public SearchTasksController(ITaskSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("page")]
        public async Task<TaskListPageDto> GetTaskListPage([FromBody] TaskSearchFilterDto filter)
        {
            var result = await _searchService.GetTasksAsync(filter);
            return result;
        }
    }
}