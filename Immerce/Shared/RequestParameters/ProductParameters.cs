namespace Immerce.Shared
{
    public sealed class ProductParameters : BaseParameters
    {
        public string? SearchString { get; set; } = string.Empty;
    }

    public sealed class ProductQueryParams
    {
        public string? SearchString { get; set; }

        public int Page { get; set; }
    }
}
