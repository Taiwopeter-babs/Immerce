using Immerce.Shared;
using System.Net.Http.Json;

namespace Immerce.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }


        public List<Category> Categories { get; set; } =  new();

        public async Task GetCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/v1/categories");

            if (response != null && response.Data != null)
                Categories = response.Data;
        }
    }
}
