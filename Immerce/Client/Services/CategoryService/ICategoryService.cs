using Immerce.Shared;

namespace Immerce.Client.Services
{
    public interface ICategoryService
    {
        Task GetCategories();

        //Task<ServiceResponse<Category>?> GetCategory(int id);

        /// <summary>
        /// Public property to access all categories
        /// </summary>
        List<Category> Categories { get; set; }
    }
}
