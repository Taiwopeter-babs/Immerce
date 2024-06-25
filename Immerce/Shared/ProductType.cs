using System.ComponentModel.DataAnnotations;


namespace Immerce.Shared
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
