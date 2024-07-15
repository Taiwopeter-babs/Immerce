using Immerce.Shared;

namespace Immerce.Client.Services
{
    /// <summary>
    /// Contract for CartService
    /// </summary>
    public interface ICartService
    {
        event EventHandler? CartChanged;

        string CartName { get; }

        Task AddToCart(CartItem item);

        Task<List<CartItem>> GetCartItems();

        Task<List<CartProductDto>> GetCartProducts();

        Task RemoveCartItem(int productId, int productTypeId);

        Task UpdateQuantity(CartProductDto product);
    }
}
