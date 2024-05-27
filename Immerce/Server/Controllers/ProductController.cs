using Immerce.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DatabaseContext _dbContext;
        public ProductController(DatabaseContext dbContext) 
        {
            _dbContext = dbContext;
        }
        

        [HttpGet]
        public async Task<ActionResult<List<Product>?>> GetProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }
    }
}
