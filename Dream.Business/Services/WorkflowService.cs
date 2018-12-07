using System;
using System.Threading.Tasks;
using Dream.Business.Abstract;
using Dream.Business.Extensions;
using Dream.Business.Services.TaskFlowStrategies;
using Dream.Business.TransferModels;
using Dream.DataAccess;
using Dream.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dream.Business.Services
{
    /// <summary>
    /// Task workflow implementation.
    /// </summary>
    public class WorkflowService : IWorkflowService
    {
        private readonly DreamContext _dbContext;

        public WorkflowService(DreamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(TaskDto task, string error)> StartTaskAsync(int taskId)
        {
            ITaskFlowStrategy strategy = new StartTaskStrategy();
            return await ChangeFlowStatusAsync(taskId, strategy);
        }

        public async Task<(TaskDto task, string error)> ReopenTaskAsync(int taskId)
        {
            ITaskFlowStrategy strategy = new ReopenTaskStrategy();
            return await ChangeFlowStatusAsync(taskId, strategy);
        }

        public async Task<(TaskDto task, string error)> CloseTaskAsync(int taskId)
        {
            ITaskFlowStrategy strategy = new CloseTaskStrategy();
            return await ChangeFlowStatusAsync(taskId, strategy);
        }

        private async Task<(TaskDto task, string error)> ChangeFlowStatusAsync(int taskId, ITaskFlowStrategy strategy)
        {
            // validate
            var dbTask = await _dbContext.Set<WorkTask>().FirstOrDefaultAsync(t => t.Id == taskId);
            if (dbTask == null)
                return (null, "task not found");
            var dbProject = await _dbContext.Set<Project>().FirstOrDefaultAsync(t => t.Id == dbTask.ProjectId);
            if (dbProject == null)
                return (null, "project not found");

            var validation = strategy.ValidateStatus(dbTask.FlowStatus);
            if (!validation.IsSuccess)
                return (null, validation.ErrorText);

            // modify
            dbTask.FlowStatus = strategy.ChangedStatus();
            dbTask.ModifyDate = DateTime.UtcNow;

            // store
            await _dbContext.SaveChangesAsync();

            // return
            return (dbTask.AsTask(), null);
        }
    }
}