using Dream.Business.TransferModels;
using Dream.Models.Entities;

namespace Dream.Business.Extensions
{
    /// <summary>
    /// Converts database entities to transfer data objects.
    /// </summary>
    public static class TransferConverterExtension
    {
        public static ProjectDto AsProject(this Project entity)
        {
            return new ProjectDto()
            {
                Description = entity.Description,
                Title = entity.Title,
                ModifyDate = entity.ModifyDate,
                CreateDate = entity.CreateDate,
                Id = entity.Id
            };
        }

        public static TaskDto AsTask(this WorkTask entity)
        {
            return new TaskDto()
            {
                Description = entity.Description,
                Title = entity.Title,
                ModifyDate = entity.ModifyDate,
                CreateDate = entity.CreateDate,
                Id = entity.Id,
                Priority = entity.Priority,
                FlowStatus = entity.FlowStatus,
                ProjectId = entity.ProjectId
            };
        }
    }
}