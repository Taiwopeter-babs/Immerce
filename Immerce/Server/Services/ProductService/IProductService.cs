namespace Immerce.Server.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);

        Task<ServiceResponse<Product>> GetProductAsync(int id);

        Task<ServiceResponse<ProductSearchDto>> SearchProducts(ProductParameters productParams);

        Task<ServiceResponse<List<string>?>> GetProductsSuggestions(string? searchString);

        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    }
}
