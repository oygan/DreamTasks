using System;
using Dream.Models.Enums;

namespace Dream.Models.Entities
{
    /// <summary>
    /// Task database model.
    /// </summary>
    public class WorkTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public TaskStatuses FlowStatus { get; set; }
        public Project Project { get; set; }
    }
}