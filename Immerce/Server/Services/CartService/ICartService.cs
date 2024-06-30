using Immerce.Shared.Dto;

namespace Immerce.Server.Services
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductDto>>>
            GetCartProductsAsync(List<CartItem> cartItems);
    }
}
