namespace Remont.WebUI.Controllers.Api
{
    public class PageInfoRequest<TKey>
    {
        public TKey Id { get; set; }

        public int PageIndex { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}