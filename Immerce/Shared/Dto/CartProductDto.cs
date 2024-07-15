

namespace Immerce.Shared
{
    public class CartProductDto
    {
        public int ProductId { get; set; }

        public string Title { get; set; } = null!;

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
