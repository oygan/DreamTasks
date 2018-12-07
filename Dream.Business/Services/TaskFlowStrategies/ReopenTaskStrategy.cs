using Dream.Models.Enums;

namespace Dream.Business.Services.TaskFlowStrategies
{
    /// <summary>
    /// Implements the 'reopen' strategy in workflow for tasks.
    /// </summary>
    class ReopenTaskStrategy : ITaskFlowStrategy    
    {
        public ValidateItem ValidateStatus(TaskStatuses flowStatus)
        {
            if (flowStatus == TaskStatuses.InProgress)
                return new ValidateItem();
            return new ValidateItem() { ErrorText = "Available statuses: InProgress", IsSuccess = false };
        }

        public TaskStatuses ChangedStatus()
        {
            return TaskStatuses.Created;
        }
    }
}