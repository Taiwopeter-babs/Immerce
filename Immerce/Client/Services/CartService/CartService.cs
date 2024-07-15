using Blazored.LocalStorage;
using Immerce.Shared;
using System.Net.Http.Json;

namespace Immerce.Client.Services
{

    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public CartService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;

        }

        #region Properties

        public string CartName { get; } = "immerce_cart";


        /// <summary>
        /// Event for monitoring changes to cart
        /// </summary>
        public event EventHandler? CartChanged;

        #endregion Properties

        /// <summary>
        /// Add an item to the cart in local storage
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddToCart(CartItem item)
        {
            List<CartItem> cart = await CheckCart();

            CartItem? cartItem = cart
                    .Where(c => c.ProductId == item.ProductId
                        && c.ProductTypeId == item.ProductTypeId)
                    .FirstOrDefault();

            // Quantity Increment
            if (cartItem != null)
                cartItem.Quantity += 1;
            else
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

        public async Task<List<CartProductDto>> GetCartProducts()
        {
            var cart = await CheckCart();

            var response = await _httpClient.PostAsJsonAsync("api/v1/carts/products", cart);

            var content = await response.Content
                    .ReadFromJsonAsync<ServiceResponse<List<CartProductDto>>>();

            return content!.Data!;
        }

        public async Task RemoveCartItem(int productId, int productTypeId)
        {
            var (cart, cartItem) = await CheckCartItem(productId, productTypeId);

            if (cart is null) return;

            if (cartItem != null)
            {
                cart.Remove(cartItem);

                // update local storage
                await _localStorage.SetItemAsync(CartName, cart);

                // invoke event handler
                CartChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task UpdateQuantity(CartProductDto product)
        {
            var (cart, cartItem) = await CheckCartItem(product.ProductId, product.ProductTypeId);

            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;

                // update local storage
                await _localStorage.SetItemAsync(CartName, cart);
            }
        }

        private async Task<(List<CartItem>?, CartItem?)> CheckCartItem(int productId, int productTypeId)
        {
            List<CartItem>? cart = await CheckCart();

            if (cart is null) 
                return (null, null);

            CartItem? cartItem = cart.Find(c =>
                    c.ProductId == productId
                    && c.ProductTypeId == productTypeId
                 );

            return (cart, cartItem);
        }

        private async Task<List<CartItem>> CheckCart()
        {
            List<CartItem> cart;

            cart = await _localStorage.GetItemAsync<List<CartItem>>(CartName) ?? new();

            return cart;

        }
    }
}
