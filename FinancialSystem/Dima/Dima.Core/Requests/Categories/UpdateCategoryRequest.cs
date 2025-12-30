using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Invalid title")]
        [MaxLength(80, ErrorMessage = "Max lenght is 80")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invalid description")]
        public string Description { get; set; } = string.Empty;
    }
}
