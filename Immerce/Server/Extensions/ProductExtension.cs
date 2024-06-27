namespace Immerce.Server.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<Product> GetProductsByPage(this IQueryable<Product> product,
                int page, int pageSize)
        {
            int pageNumber = page < 1 ? 0 : page - 1;

            return product.Skip(pageNumber * pageSize).Take(pageSize);
        }
    }
}
