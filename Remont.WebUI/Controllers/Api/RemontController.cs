using System.Collections.Generic;
using System.Web.Http;
using System.Web.UI;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class Response<TItem, TKey>
    {
        public IList<TItem> Items { get; set; }

        public TItem Item { get; set; }

        public PageInfoRequest<TKey> PageInfoRequest { get; set; }
    }

    public class PageInfoRequest<TKey>
    {
        public TKey Id { get; set; }

        public int PageIndex { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }

    public abstract class RemontController<TItem, TKey> : ApiController
        where TItem : BaseItem<TKey>
    {
        private readonly IRepository<TItem, TKey> _repository;

        protected RemontController(IRepository<TItem, TKey> repository)
        {
            _repository = repository;
        }

        public virtual Response<TItem, TKey> Get([FromUri]PageInfoRequest<TKey> pageInfoRequest)
        {
            if (pageInfoRequest.Id.Equals(default(TKey)))
            {
                int totalItems;
                int totalPages;
                int pageIndexOut;

                var items = _repository.Get(pageInfoRequest.PageIndex, out totalItems, out totalPages, out pageIndexOut);

                pageInfoRequest.TotalItems = totalItems;
                pageInfoRequest.TotalPages = totalPages;
                pageInfoRequest.PageIndex = pageIndexOut;

                return new Response<TItem, TKey>
                {
                    Items = items,
                    PageInfoRequest = pageInfoRequest
                };
            }

            return new Response<TItem, TKey>
            {
                Item = _repository.Find(pageInfoRequest.Id),
                PageInfoRequest = pageInfoRequest
            };
        }

        public virtual TKey Post(TItem item)
        {
            return _repository.AddOrUpdate(item);
        }

        public void Delete(TKey itemId)
        {
            _repository.Delete(itemId);
        }
    }
}
