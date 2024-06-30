using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Immerce.Shared
{
    /// <summary>
    /// A product could have multiple variants. For example, a book could be available
    /// as an e-book, paperback etc...
    /// </summary>
    public class ProductVariant
    {
        [JsonIgnore]
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }

        public ProductType ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OriginalPrice { get; set; }
    }
}
