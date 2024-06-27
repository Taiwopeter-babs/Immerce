namespace Immerce.Shared
{
    public class ProductSearchDto
    {
        public List<Product> Products { get; set; } = new();

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
