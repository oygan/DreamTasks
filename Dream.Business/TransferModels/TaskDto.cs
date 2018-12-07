using System;
using Dream.Models.Enums;

namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Task data transfer model.
    /// </summary>
    public class TaskDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public TaskStatuses FlowStatus { get; set; }
    }
}