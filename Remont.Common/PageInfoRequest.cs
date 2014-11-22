namespace Remont.Common
{
    public class PageInfoRequest
    {
        public int TableId { get; set; }

        public int Id { get; set; }

        public int PageIndex { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public string Action { get; set; }
    }
}