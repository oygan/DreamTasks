using System;
using System.ComponentModel.DataAnnotations;
using Dream.Models.Enums;

namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Searching params.
    /// </summary>
    public class TaskSearchFilterDto
    {
        /// <summary>
        /// Page number, start with '0'
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value equal or bigger than 0")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Page Size.
        /// </summary>
        [Range(1, 100, ErrorMessage = "Please enter a value from 1 to 100")]
        public int PageSize { get; set; }

        /// <summary>
        /// Project Id filter.
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Date Minimum filter.
        /// </summary>
        public DateTime? CreateDateMin { get; set; }

        /// <summary>
        /// Date Maximum filter.
        /// </summary>
        public DateTime? CreateDateMax { get; set; }

        /// <summary>
        /// Flow status filter.
        /// </summary>
        [EnumDataType(typeof(TaskStatuses), ErrorMessage = "Please enter a value from 0 to 2")]
        public TaskStatuses? FlowStatus { get; set; }

        /// <summary>
        /// Priority filter.
        /// </summary>
        [Range(0, 50, ErrorMessage = "Please enter a value from 0 to 50")]
        public int? Priority { get; set; }

        /// <summary>
        /// Order field.
        /// </summary>
        [EnumDataType(typeof(OrderTaskEnum), ErrorMessage = "Please enter a value from 0 to 2")]
        public OrderTaskEnum Order { get; set; }
    }
}