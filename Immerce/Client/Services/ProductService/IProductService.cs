using Immerce.Shared;

namespace Immerce.Client.Services
{
    public interface IProductService
    {
        /// <summary>
        /// An event that is invoked when the products page parameter is changed
        /// on the page to get products by <b>categoryUrl</b>
        /// </summary>
        event EventHandler? ProductsChanged;
        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>?> GetProduct(int id);

        /// <summary>
        /// Public property to access all products
        /// </summary>
        List<Product> Products { get; set; }
    }
}
