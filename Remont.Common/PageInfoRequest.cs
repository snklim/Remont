namespace Remont.Common
{
    public class PageInfoRequest<TKey>
    {
        public TKey TableId { get; set; }

        public TKey Id { get; set; }

        public int PageIndex { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}