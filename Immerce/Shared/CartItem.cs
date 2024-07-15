

namespace Immerce.Shared
{
    /// <summary>
    /// Stores reference to product
    /// </summary>
    public class CartItem
    {
        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
