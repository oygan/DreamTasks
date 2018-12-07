using Dream.Models.Enums;

namespace Dream.Business.Services.TaskFlowStrategies
{
    /// <summary>
    /// Implements the 'start' strategy in workflow for tasks.
    /// </summary>
    class StartTaskStrategy : ITaskFlowStrategy
    {
        public ValidateItem ValidateStatus(TaskStatuses flowStatus)
        {
            if (flowStatus == TaskStatuses.Created)
                return new ValidateItem();
            return new ValidateItem() { ErrorText = "Available statuses: Created", IsSuccess = false };
        }

        public TaskStatuses ChangedStatus()
        {
            return TaskStatuses.InProgress;
        }
    }
}