using Immerce.Shared;
using Refit;

namespace Immerce.Client.Services;

public interface IProductApi
{
    /// <summary>
    /// <b>Get a single product</b>
    /// </summary>
    [Get("/products/{id}")]
    Task<ServiceResponse<Product?>> GetProduct(int id);

    /// <summary>
    /// <b>Get featured products</b>
    /// </summary>
    [Get("/products/featured")]
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();

    /// <summary>
    /// <b>Get products by category</b>
    /// </summary>
    [Get("/products/{categoryUrl}")]
    Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);

    /// <summary>
    /// <b>Get search products suggestions</b>
    /// </summary>
    [Get("/products/search/suggestions")]
    Task<ServiceResponse<List<string>>> GetProductsSuggestions([Query]ProductQueryParams queryParams);

    /// <summary>
    /// <b>Get product search results.</b>
    /// </summary>
    [Get("/products/search")]
    Task<ServiceResponse<ProductSearchDto>> SearchProducts([Query] ProductQueryParams queryParams);
}
