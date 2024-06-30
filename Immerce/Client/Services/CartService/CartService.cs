using Blazored.LocalStorage;
using Immerce.Shared;
using Immerce.Shared.Dto;
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
            List<CartItem> cart = await CheckCart();

            CartItem? cartItem = cart
                    .Where(c => c.ProductId == item.ProductId
                        && c.ProductTypeId == item.ProductTypeId)
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

        public async Task<List<CartProductDto>> GetCartProducts()
        {
            var cart = await CheckCart();

            var response = await _httpClient.PostAsJsonAsync("api/v1/carts/products", cart);

            var content = await response.Content
                    .ReadFromJsonAsync<ServiceResponse<List<CartProductDto>>>();

            return content.Data;
        }

        private async Task<List<CartItem>> CheckCart()
        {
            List<CartItem> cart;

            cart = await _localStorage.GetItemAsync<List<CartItem>>(CartName) ?? new();

            return cart;

        }
    }
}
