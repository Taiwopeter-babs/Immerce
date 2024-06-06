using Immerce.Server.Services;
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

        [HttpGet("{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>?>> GetProductsByCategory(string categoryUrl)
        {
            var response = await _productService.GetProductsByCategoryAsync(categoryUrl);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<Product>>?>> GetProductsBySearch([FromQuery]string searchString)
        {
            var response = await _productService.SearchProducts(searchString);

            return Ok(response);
        }

        // GET api/v1/products/search/suggestions[?searchString=stringValue]
        [HttpGet("search/suggestions")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductsSuggestions([FromQuery] string? searchString)
        {
            var response = await _productService.GetProductsSuggestions(searchString);

            return Ok(response);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
        {
            var response = await _productService.GetFeaturedProducts();

            return Ok(response);
        }
    }
}
