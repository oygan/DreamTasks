using System;
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
    /// CRUD for projects
    /// </summary>
    public class ProjectEditorService : IProjectEditorService
    {
        private readonly DreamContext _dbContext;

        public ProjectEditorService(DreamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectDto> AddProjectAsync(NewProjectDto project)
        {
            // convert
            Project dbProject = new Project()
            {
                Description = project.Description,
                Title = project.Title,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
            };

            // store
            var addedProject = await _dbContext.AddAsync(dbProject);
            await _dbContext.SaveChangesAsync();

            // return
            return addedProject.Entity.AsProject();
        }

        public async Task<(ProjectDto project, string error)> UpdateProjectAsync(int projectId, NewProjectDto project)
        {
            // get
            var dbProject = await _dbContext.Set<Project>().FirstOrDefaultAsync(t => t.Id == projectId);
            if (dbProject == null)
                return (null, "project not found");

            // modify
            dbProject.Description = project.Description;
            dbProject.Title = project.Title;
            dbProject.ModifyDate = DateTime.UtcNow;
            
            // store
            await _dbContext.SaveChangesAsync();

            // return
            return (dbProject.AsProject(), null);
        }

        public async Task<(ProjectDto project, string error)> GetProjectAsync(int projectId)
        {
            // get
            var dbProject = await _dbContext.Set<Project>().FirstOrDefaultAsync(t => t.Id == projectId);
            if (dbProject == null)
                return (null, "project not found");

            // return
            return (dbProject.AsProject(), null);
        }

        public async Task<(ProjectDto project, string error)> DeleteProjectAsync(int projectId)
        {
            // get
            var dbProject = await _dbContext.Set<Project>().FirstOrDefaultAsync(t => t.Id == projectId);
            if (dbProject == null)
                return (null, "project not found");

            // store
            var removeProject = _dbContext.Remove(dbProject);
            await _dbContext.SaveChangesAsync();

            // return
            return (removeProject.Entity.AsProject(), null);
        }
    }
}
