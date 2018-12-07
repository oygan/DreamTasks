namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Editable fields for task model.
    /// </summary>
    public class NewTaskDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}