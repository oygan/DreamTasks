using System.ComponentModel.DataAnnotations;

namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Editable fields for task model.
    /// </summary>
    public class NewTaskDto
    {
        /// <summary>
        /// Project Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [StringLength(50, ErrorMessage = "Value cannot be longer than 50 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(250, ErrorMessage = "Value cannot be longer than 250 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        [Range(0, 50, ErrorMessage = "Value range is from 0 to 50")]
        public int Priority { get; set; }
    }
}