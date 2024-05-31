using Immerce.Shared;

namespace Immerce.Client.Services
{
    public interface IProductService
    {
        Task GetProducts();

        Task<ServiceResponse<Product>?> GetProduct(int id);

        /// <summary>
        /// Public property to access all products
        /// </summary>
        List<Product> Products { get; set; }
    }
}
