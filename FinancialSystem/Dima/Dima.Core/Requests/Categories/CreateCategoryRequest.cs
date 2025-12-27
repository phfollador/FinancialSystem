using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Invalid title")]
        [MaxLength(80, ErrorMessage = "Max lenght is 80")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invalid description")]
        public string Description { get; set; } = string.Empty;
    }
}
