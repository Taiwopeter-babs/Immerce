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
            PageParameters = new PageParameters();

        }

        #region Properties

        public List<Product> Products { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public PageParameters PageParameters { get; set; }

        /// <summary>
        /// Event for monitoring the parameter change to navigate products by categoryUrl
        /// </summary>
        public event EventHandler? ProductsListUrlChanged;

        #endregion


        #region Public Methods

        public async Task<ServiceResponse<Product?>> GetProduct(int id)
        {
            string apiUrl = $"api/v1/products/{id}";

            var response = await QueryGetAsync<ServiceResponse<Product?>>(apiUrl);

            return response;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            string apiUrl = categoryUrl == null
                    ? "api/v1/products/featured"
                    : $"api/v1/products/{categoryUrl}";

            var response = await QueryGetAsync<ServiceResponse<List<Product>>>(apiUrl);

            if (response != null && response.Data != null)
                Products = response.Data;

            if (Products.Count == 0)
                Message = "No Products Found";

            /// Invoke event handler
            ProductsListUrlChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task<List<string>?> GetProductsSuggestions(string? searchString = null)
        {
            string apiUrl = $"api/v1/products/search/suggestions?searchString={searchString}";

            var response = await QueryGetAsync<ServiceResponse<List<string>>>(apiUrl);

            return response.Data;
        }

        public async Task SearchProducts(string? searchString)
        {
            // This region is unimplemented
            // but is useful for parsing page query parameters
            #region GetQueryParams

            ////Dictionary<string, string?> queryParams = new();

            ////Type pageObject = PageParameters.GetType();

            ////PropertyInfo[] props = pageObject.GetProperties();


            ////foreach (var prop in props)
            ////{
            ////    if (prop.Name == "PageNumber")
            ////    {
            ////        queryParams.Add(prop.Name, prop.GetValue(pageObject)!.ToString());
            ////        break;
            ////    }   
            ////}        

            ////queryParams.Add("searchString", searchString);

            #endregion

            PageParameters.LastSearchString = searchString;

            string queryString = $"searchString={searchString}&page={PageParameters.Page}";

            string apiUrl = $"api/v1/products/search?{queryString}";

            var response = await QueryGetAsync<ServiceResponse<ProductSearchDto>>(apiUrl);

            if (response != null && response.Data != null)
            {
                Products = response.Data.Products;
                PageParameters.Page = response.Data.Page;
                PageParameters.TotalPages = response.Data.TotalPages;
            }

            if (Products.Count == 0)
                Message = "No products found";

            /// Invoke event handler
            ProductsListUrlChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion


        #region Private Methods

        private async Task<T> QueryGetAsync<T>(string apiUrl)
        {
            var result = await _httpClient.GetFromJsonAsync<T>(apiUrl);

            return result!;
        }

        #endregion
    }
}
