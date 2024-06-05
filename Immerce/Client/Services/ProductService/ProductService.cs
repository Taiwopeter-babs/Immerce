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

        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Event for monitoring the parameter change to navigate products by categoryUrl
        /// </summary>
        public event EventHandler? ProductsListUrlChanged;

        public async Task<ServiceResponse<Product?>> GetProduct(int id)
        {
            string apiUrl = $"api/v1/products/{id}";

            var response = await QueryApiAsync<ServiceResponse<Product?>>(apiUrl);

            return response;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            string apiUrl = categoryUrl == null ? "api/v1/products" : $"api/v1/products/{categoryUrl}";

            var response = await QueryApiAsync<ServiceResponse<List<Product>>>(apiUrl);

            if (response != null && response.Data != null)
                Products = response.Data;

            /// Invoke event handler
            ProductsListUrlChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task<List<string>?> GetProductsSuggestions(string? searchString = null)
        {
            string apiUrl = $"api/v1/products/search/suggestions?searchString={searchString}";

            var response = await QueryApiAsync<ServiceResponse<List<string>>>(apiUrl);

            return response.Data;
        }

        public async Task SearchProducts(string? searchString = null)
        {
            string apiUrl = $"api/v1/products/search?searchString={searchString}";

            var response = await QueryApiAsync<ServiceResponse<List<Product>>>(apiUrl);

            if (response != null && response.Data != null)
                Products = response.Data;

            if (Products.Count == 0)
                Message = "No products found";

            /// Invoke event handler
            ProductsListUrlChanged?.Invoke(this, EventArgs.Empty);
        }

        private async Task<T> QueryApiAsync<T>(string apiUrl)
        {
            var result = await _httpClient.GetFromJsonAsync<T>(apiUrl);

            return result!;
        }
    }
}
