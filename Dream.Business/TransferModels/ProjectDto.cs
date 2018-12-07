using System;

namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Project data transfer model.
    /// </summary>
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}