namespace Immerce.Shared
{
    public class BaseParameters
    {
        private int _pageSize = 3;

        private const int _maxPageSize = 10;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = (value <= _maxPageSize) ? value : _pageSize; }
        }
    }
}
