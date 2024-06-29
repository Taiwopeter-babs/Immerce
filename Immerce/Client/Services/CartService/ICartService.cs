using Immerce.Shared;

namespace Immerce.Client.Services
{
    public interface ICartService
    {
        event EventHandler? CartChanged;

        string CartName { get; }

        Task AddToCart(CartItem item);

        Task<List<CartItem>> GetCartItems();
    }
}
