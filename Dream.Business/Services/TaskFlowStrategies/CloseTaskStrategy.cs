using Dream.Models.Enums;

namespace Dream.Business.Services.TaskFlowStrategies
{
    /// <summary>
    /// Implements the 'close' strategy in workflow for tasks.
    /// </summary>
    class CloseTaskStrategy : ITaskFlowStrategy
    {
        public ValidateItem ValidateStatus(TaskStatuses flowStatus)
        {
            if (flowStatus == TaskStatuses.InProgress || flowStatus == TaskStatuses.Created)
                return new ValidateItem();
            return new ValidateItem() { ErrorText = "available status: InProgress or Created", IsSuccess = false };
        }

        public TaskStatuses ChangedStatus()
        {
            return TaskStatuses.Closed;
        }
    }
}