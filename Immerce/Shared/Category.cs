using System.ComponentModel.DataAnnotations;

namespace Immerce.Shared
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Url { get; set; }
    }
}
