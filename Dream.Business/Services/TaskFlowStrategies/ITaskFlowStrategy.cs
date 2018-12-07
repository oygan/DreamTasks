using Dream.Models.Enums;

namespace Dream.Business.Services.TaskFlowStrategies
{
    /// <summary>
    /// Strategy interface for workflow.
    /// </summary>
    internal interface ITaskFlowStrategy
    {
        TaskStatuses ChangedStatus();
        ValidateItem ValidateStatus(TaskStatuses flowStatus);
    }
}