namespace Immerce.Shared
{
    /// <summary>
    /// Parameters for client pagination
    /// </summary>
    public sealed class PageParameters
    {
        public string? LastSearchString { get; set; } = string.Empty;

        public int Page { get; set; } = 1;

        public int TotalPages { get; set; } = 0;
    }
}
