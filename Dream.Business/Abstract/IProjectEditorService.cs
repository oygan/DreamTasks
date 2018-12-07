using System;
using System.Threading.Tasks;
using Dream.Business.TransferModels;

namespace Dream.Business.Abstract
{
    /// <summary>
    /// Crud methods for projects.
    /// </summary>
    public interface IProjectEditorService
    {
        Task<ProjectDto> AddProjectAsync(NewProjectDto project);
        Task<(ProjectDto project, string error)> UpdateProjectAsync(int projectId, NewProjectDto project);
        Task<(ProjectDto project, string error)> GetProjectAsync(int projectId);
        Task<(ProjectDto project, string error)> DeleteProjectAsync(int projectId);
    }
}