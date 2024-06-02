
using Immerce.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _dbContext;

        public CategoryService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            var response = new ServiceResponse<List<Category>>()
            {
                Data = categories
            };

            return response;
        }
    }
}
