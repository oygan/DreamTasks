using System.ComponentModel.DataAnnotations;

namespace Dream.Business.TransferModels
{
    /// <summary>
    /// Editable fields for project model.
    /// </summary>
    public class NewProjectDto
    {
        /// <summary>
        /// Project title
        /// </summary>
        [StringLength(50, ErrorMessage = "Value cannot be longer than 50 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Project description
        /// </summary>
        [StringLength(250, ErrorMessage = "Value cannot be longer than 250 characters.")]
        public string Description { get; set; }
    }
}
