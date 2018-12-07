using System;
using Dream.Models.Enums;

namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Searching params.
    /// </summary>
    public class TaskSearchFilterDto
    {
        /// <summary>
        /// page start with '0'
        /// </summary>
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int? ProjectId { get; set; }
        public DateTime? CreateDateMin { get; set; }
        public DateTime? CreateDateMax { get; set; }
        public TaskStatuses? FlowStatus { get; set; }
        public int? Priority { get; set; }

        public OrderTaskEnum Order { get; set; }
    }
}