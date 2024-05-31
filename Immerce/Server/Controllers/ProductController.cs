using Immerce.Server.Data;
using Immerce.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }
        

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>?>> GetProducts()
        {
            var response = await _productService.GetProductsAsync();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Product?>>> GetProduct(int id)
        {
            var response = await _productService.GetProductAsync(id);

            return Ok(response);
        }
    }
}
