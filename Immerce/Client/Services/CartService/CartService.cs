using Blazored.LocalStorage;
using Immerce.Shared;

namespace Immerce.Client.Services
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public  string CartName { get; } = "immerce_cart";



        /// <summary>
        /// Event for monitoring changes to cart
        /// </summary>
        public event EventHandler? CartChanged;

        /// <summary>
        /// Add an item to the cart in local storage
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddToCart(CartItem item)
        {
            List<CartItem> cart = await GetCartItems();

            CartItem? cartItem = cart
                    .Where(c => c.ProductId == item.ProductId)
                    .FirstOrDefault();

            if (cartItem != null)
                return;

            cart.Add(item);

            await _localStorage.SetItemAsync(CartName, cart);

            // invoke event handler
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Get items from cart in local storage
        /// </summary>
        /// <returns></returns>
        public async Task<List<CartItem>> GetCartItems()
        {
            List<CartItem> cart = await CheckCart();

            return cart;
        }

        private async Task<List<CartItem>> CheckCart()
        {
            List<CartItem> cart;

            cart = await _localStorage.GetItemAsync<List<CartItem>>(CartName) ?? new();

            return cart;

        }
    }
}
