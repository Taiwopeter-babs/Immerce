using Immerce.Shared;

namespace Immerce.Client.Services
{
    /// <summary>
    /// Product service interface for client interaction
    /// </summary>
    public interface IProductService
    {
        #region Properties
        /// <summary>
        /// An event that is invoked when the products page parameter is changed
        /// on the page to get products by <b>categoryUrl</b>
        /// </summary>
        event EventHandler? ProductsListUrlChanged;

        /// <summary>
        /// Public property to access all products
        /// </summary>
        List<Product> Products { get; set; }

        /// <summary>
        /// Notifier for product search result
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Page parameters
        /// </summary>
        PageParameters PageParameters { get; set; }

        #endregion

        #region Methods

        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product?>> GetProduct(int id);

        Task SearchProducts(string? searchString = null);

        Task<List<string>?> GetProductsSuggestions(string? searchString = null);

        #endregion 
    }
}
