using System;
using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Extensions;
using Dream.Business.TransferModels;
using Dream.DataAccess;
using Dream.Models.Entities;
using Dream.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dream.Business.Services
{
    /// <summary>
    /// CRUD for tasks
    /// </summary>
    public class TaskEditorService : ITaskEditorService
    {
        private readonly DreamContext _dbContext;

        public TaskEditorService(DreamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(TaskDto task, string error)> AddTaskAsync(NewTaskDto task)
        {
            // get
            var dbProject = await _dbContext.Set<Project>().FirstOrDefaultAsync(t => t.Id == task.ProjectId);
            if (dbProject == null)
                return (null, "project not found");

            // convert
            WorkTask dbTask = new WorkTask()
            {
                Description = task.Description,
                Title = task.Title,
                Priority = task.Priority,
                ProjectId = task.ProjectId,
                FlowStatus = TaskStatuses.Created,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
            };

            // store
            var addedTask = await _dbContext.AddAsync(dbTask);
            await _dbContext.SaveChangesAsync();

            // return
            return (addedTask.Entity.AsTask(), null);
        }

        public async Task<(TaskDto task, string error)> UpdateTaskAsync(int taskId, NewTaskDto task)
        {
            // validate
            var dbProject = await _dbContext.Set<Project>().FirstOrDefaultAsync(t => t.Id == task.ProjectId);
            if (dbProject == null)
                return (null, "project not found");

            var dbTask = await _dbContext.Set<WorkTask>().FirstOrDefaultAsync(t => t.Id == taskId);
            if (dbTask == null)
                return (null, "task not found");
            if (dbTask.FlowStatus == TaskStatuses.Closed)
                return (null, "closed task not editable");

            // modify
            dbTask.Description = task.Description;
            dbTask.Title = task.Title;
            dbTask.Priority = task.Priority;
            dbTask.ProjectId = task.ProjectId;
            dbTask.ModifyDate = DateTime.UtcNow;

            // store
            await _dbContext.SaveChangesAsync();

            // return
            return (dbTask.AsTask(), null);
        }

        public async Task<(TaskDto task, string error)> GetTaskAsync(int taskId)
        {
            // get
            var dbTask = await _dbContext.Set<WorkTask>().FirstOrDefaultAsync(t => t.Id == taskId);
            if (dbTask == null)
                return (null, "task not found");

            // return
            return (dbTask.AsTask(), null);
        }

        public async Task<(TaskDto task, string error)> DeleteTaskAsync(int taskId)
        {
            // get
            var dbTask = await _dbContext.Set<WorkTask>().FirstOrDefaultAsync(t => t.Id == taskId);
            if (dbTask == null)
                return (null, "task not found");

            // store
            var removeTask = _dbContext.Remove(dbTask);
            await _dbContext.SaveChangesAsync();

            // return
            return (removeTask.Entity.AsTask(), null);
        }
    }
}