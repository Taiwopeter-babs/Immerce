namespace Immerce.Server.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);

        Task<ServiceResponse<Product>> GetProductAsync(int id);

        Task<ServiceResponse<List<Product>>> SearchProducts(string searchString);

        Task<ServiceResponse<List<string>?>> GetProductsSuggestions(string? searchString);
    }
}
