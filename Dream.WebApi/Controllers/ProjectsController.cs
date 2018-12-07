using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Extensions;
using Dream.Business.TransferModels;
using Microsoft.AspNetCore.Mvc;

namespace Dream.WebApi.Controllers
{
    /// <summary>
    /// This controller contains methods to create, read, update and delete projects.
    /// </summary>
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectEditorService _editorService;

        public ProjectsController(IProjectEditorService editorService) 
        {
            _editorService = editorService;
        }

        [HttpGet("{projectId}")]
        public async Task<ProjectDto> GetProject(int projectId)
        {
            var result = await _editorService.GetProjectAsync(projectId);
            return result.project.CheckError(Response, result.error);
        }

        [HttpPost()]
        public async Task<ProjectDto> PostProject([FromBody] NewProjectDto project)
        {
            return await _editorService.AddProjectAsync(project);
        }

        [HttpPut("{projectId}")]
        public async Task<ProjectDto> PutProject(int projectId, [FromBody] NewProjectDto project)
        {
            var result = await _editorService.UpdateProjectAsync(projectId, project);
            return result.project.CheckError(Response, result.error);
        }

        [HttpDelete("{projectId}")]
        public async Task<ProjectDto> DeleteProject(int projectId)
        {
            var result = await _editorService.DeleteProjectAsync(projectId);
            return result.project.CheckError(Response, result.error);
        }
    }
}