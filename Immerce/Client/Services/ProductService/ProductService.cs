using Immerce.Shared;
using System.Net.Http.Json;


namespace Immerce.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public List<Product> Products { get; set; } = new();

        /// <summary>
        /// Event for monitoring the parameter change to navigate products by categoryUrl
        /// </summary>
        public event EventHandler? ProductsListUrlChanged;

        public async Task<ServiceResponse<Product>?> GetProduct(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>?>($"api/v1/products/{id}");

            return response;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            string apiUrl = categoryUrl == null ? "api/v1/products" : $"api/v1/products/{categoryUrl}";

            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(apiUrl);

            if (response != null && response.Data != null)
                Products = response.Data;

            /// Invoke event handler
            ProductsListUrlChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
