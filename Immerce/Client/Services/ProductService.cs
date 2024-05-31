
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

        public async Task<ServiceResponse<Product>?> GetProduct(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>?>($"api/v1/products/{id}");

            return response;
        }

        public async Task GetProducts()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/v1/products");

            if (response != null && response.Data != null)
                Products = response.Data;
        }
    }
}
