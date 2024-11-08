using XDev_UnitWork.Custom;

namespace XDev_UnitWork.DTO
{
    public class PaginationDTO
    {
        private const int page = 1;
        private const int pageSize = 10;
        private const OrderDirection order = OrderDirection.ascending;
        public int Page { get; set; } = page;
        public int PageSize { get; set; } = pageSize;
        public string Filter { get; set; }
        public string SortField { get; set; }
        public OrderDirection SortOrder { get; set; } = order;

        public static ValueTask<PaginationDTO> BindAsync(HttpContext context)
        {
            var orderstring = context.GetValueOrDefault(nameof(SortOrder), string.Empty);

            var result = new PaginationDTO
            {
                Page = context.GetValueOrDefault(nameof(Page), page),
                PageSize = context.GetValueOrDefault(nameof(PageSize), pageSize),
                Filter = context.GetValueOrDefault(nameof(Filter), string.Empty),
                SortField = context.GetValueOrDefault(nameof(SortField), string.Empty),
                SortOrder = (OrderDirection)Enum.Parse(typeof(OrderDirection), orderstring.IsNullOrEmpty() ? "ascending" : orderstring),
            };

            return ValueTask.FromResult(result);
        }
    }

    public enum OrderDirection
    {
        ascending,
        descending
    }
}
