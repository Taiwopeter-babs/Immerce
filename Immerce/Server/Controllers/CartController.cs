using Immerce.Server.Services;
using Immerce.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Immerce.Server.Controllers
{
    [Route("api/v1/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductDto>>>>
            GetCartProducts([FromBody] List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProductsAsync(cartItems);

            return Ok(result);
        }
    }
}
