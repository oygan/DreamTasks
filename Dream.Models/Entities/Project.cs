using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Models.Entities
{
    /// <summary>
    /// Project database model.
    /// </summary>
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public List<WorkTask> Tasks { get; set; }
    }
}
