
using Immerce.Server.Data;
using Immerce.Server.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Immerce.Server.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly DatabaseContext _dbContext;

        public ProductService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public Methods

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var products = await FindProductsByCondition(p => p.Featured);

            return new ServiceResponse<List<Product>> { Data = products };
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            Product? product = await _dbContext.Products
                    .Include(pr => pr.Variants)
                    .ThenInclude(variant => variant.ProductType)
                    .FirstOrDefaultAsync(pr => pr.Id == id);

            var response = new ServiceResponse<Product>()
            {
                Data = product,
                Success = product != null,
                Message = product != null ? string.Empty : "Sorry, this product does not exist"
            };

            return response;
        }

        public  async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var products = await _dbContext.Products
                    .Include(pr => pr.Variants)
                    .ToListAsync();

            var response = new ServiceResponse<List<Product>>()
            {
                Data = products
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var products = await FindProductsByCondition(pr =>
                pr.Category != null && pr.Category.Url!.Equals(categoryUrl));

            var response = new ServiceResponse<List<Product>>()
            {
                Data = products
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>?>> GetProductsSuggestions(string? searchString)
        {
            // Use hashset to avoid duplicate entries
            HashSet<string> result = new();

            if (string.IsNullOrEmpty(searchString))
            {
                return new ServiceResponse<List<string>?> { Data = result.ToList() };
            }

            List<Product> products = await FindProductsByCondition(pr => 
                pr.Title.Contains(searchString) || pr.Description.Contains(searchString));

            /// <summary>
            /// Get the trimmed words from a product's description
            /// </summary>
            Func<string, IEnumerable<string>> GetWordsFromDescription = (string productDescription) =>
            {
                // Get punctuations
                char[]? punctuations = productDescription.Where(char.IsPunctuation)
                        .Distinct()
                        .ToArray();

                // split description into words and trim punctuations
                var words = productDescription.Split()
                        .Where(word => !string.IsNullOrEmpty(word))
                        .Select(word => word.Trim(punctuations));

                return words.Where(word => word.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            };

            foreach (var product in products)
            {
                if (product.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    result.Add(product.Title);

                if (product.Description == null)
                    continue;


                var words = GetWordsFromDescription(product.Description);

                result.UnionWith(words);

            }

            return new ServiceResponse<List<string>?> { Data = result.ToList() };      
        }

        /// <summary>
        /// Search for products. This feature can be improved by using a real-time connection
        /// like SignalR.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ProductSearchDto>> SearchProducts(ProductParameters productParams)
        {
            ProductSearchDto searchData;

            if (string.IsNullOrEmpty(productParams.SearchString))
            {
                searchData = new()
                {
                    Products = new(),
                    Page = productParams.Page,
                    TotalPages = 0
                };

                return new ServiceResponse<ProductSearchDto> { Data = searchData };
            }

            Expression<Func<Product, bool>> expression = (pr) => 
                pr.Title.Contains(productParams.SearchString)
                || pr.Description.Contains(productParams.SearchString);

            List<Product> products = await FindProductsByCondition(expression);

            int totalPages = (int)Math.Ceiling(products.Count / (float)productParams.PageSize);

            var searchResults = await _dbContext.Products
                    .Where(expression)
                    .Include(pr => pr.Variants)
                    .GetProductsByPage(productParams.Page, productParams.PageSize)
                    .ToListAsync();

            searchData = new()
            {
                Products = searchResults,
                Page = productParams.Page,
                TotalPages = totalPages
            };

            ServiceResponse<ProductSearchDto> response = new()
            {
                Data = searchData
            };

            return response;
        }
        #endregion

        private async Task<List<Product>> FindProductsByCondition(Expression<Func<Product, bool>> expression)
        {
            List<Product> products = await _dbContext.Products
                .Where(expression)
                .Include(pr => pr.Variants)
                .ToListAsync();

            return products;
        }

        
    }
}