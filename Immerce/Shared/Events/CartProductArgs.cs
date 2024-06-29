

namespace Immerce.Shared
{
    public class CartProductArgs : EventArgs
    {
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
    }
}
