using System.ComponentModel.DataAnnotations.Schema;


namespace Immerce.Shared
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// A product can have multiple variants 
        /// </summary>
        public List<ProductVariant> Variants { get; set; } = new();

        public bool Featured { get; set; } = false;

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

       
    }
}
