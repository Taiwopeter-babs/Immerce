
using Immerce.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly DatabaseContext _dbContext;

        public ProductService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);

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
            var products = await _dbContext.Products.ToListAsync();

            var response = new ServiceResponse<List<Product>>()
            {
                Data = products
            };

            return response;
        }
    }
}
