using Immerce.Shared;
using System.Net.Http.Json;


namespace Immerce.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductApi _productApi;

        public ProductService(HttpClient httpClient, IProductApi productApi)
        {
            _productApi = productApi;
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
            var response = await _productApi.GetProduct(id) ;

            return response;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var response = categoryUrl is null
                    ? await _productApi.GetFeaturedProducts()
                    : await _productApi.GetProductsByCategory(categoryUrl);


            if (response != null && response.Data != null)
                Products = response.Data;

            if (Products.Count == 0)
                Message = "No Products Found";

            /// Invoke event handler
            ProductsListUrlChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task<List<string>?> GetProductsSuggestions(string? searchString = null)
        {
            ProductQueryParams queryParams = new()
            {
                SearchString = searchString
            };

            var response = await _productApi.GetProductsSuggestions(queryParams);

            return response.Data;
        }

        public async Task SearchProducts(string? searchString)
        {

            PageParameters.LastSearchString = searchString;

            ProductQueryParams queryParams = new()
            {
                SearchString = searchString,
                Page = PageParameters.Page
            };

            var response = await _productApi.SearchProducts(queryParams);

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
    }
}
