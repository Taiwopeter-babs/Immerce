using Immerce.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Services
{
    public class CartService : ICartService
    {
        private readonly DatabaseContext _dbContext;

        public CartService(DatabaseContext dbContext) => _dbContext = dbContext;

        #region Public Methods

        public async Task<ServiceResponse<List<CartProductDto>>>
                GetCartProductsAsync(List<CartItem> cartItems)
        {
            ServiceResponse<List<CartProductDto>> response = new();

            List<CartProductDto> responseData = new();


            /// Gets the formmatted product data or null if product or its
            /// variant does not exist
            async Task<CartProductDto?> GetCartProductData(CartItem cartItem)
            {
                Product? product = await _dbContext.Products
                        .Where(pr => pr.Id == cartItem.ProductId)
                        .FirstOrDefaultAsync();

                if (product == null)
                    return null;

                ProductVariant? productVariant = await _dbContext.ProductVariants
                            .Where(pv =>
                                pv.ProductId == cartItem.ProductId
                                && pv.ProductTypeId == cartItem.ProductTypeId)
                            .Include(pv => pv.ProductType)
                            .FirstOrDefaultAsync();

                if (productVariant == null)
                    return null;

                return new CartProductDto()
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType!.Name,
                    ProductTypeId = productVariant.ProductTypeId
                };
            }


            foreach (CartItem cartItem in cartItems)
            {
                CartProductDto? cartProductDto = await GetCartProductData(cartItem);

                if (cartProductDto == null) continue;

                responseData.Add(cartProductDto);
            }

            response.Data = responseData;

            return response;

        }

        #endregion
    }
}
