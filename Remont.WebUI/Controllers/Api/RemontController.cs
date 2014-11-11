using System.Collections.Generic;
using System.Web.Http;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class Response<TItem, TKey>
    {
        public IList<TItem> Items { get; set; }

        public TItem Item { get; set; }

        public Request<TKey> Request { get; set; }
    }

    public class Request<TKey>
    {
        public TKey Id { get; set; }

        public int PageIndex { get; set; }

        public int TotalItems { get; set; }
    }

    public abstract class RemontController<TItem, TKey> : ApiController
        where TItem : BaseItem<TKey>
    {
        private readonly IRepository<TItem, TKey> _repository;

        protected RemontController(IRepository<TItem, TKey> repository)
        {
            _repository = repository;
        }

        public Response<TItem, TKey> Get([FromUri]Request<TKey> request)
        {
            if (request.Id.Equals(default(TKey)))
            {
                int totalItems;
                var items = _repository.Get(request.PageIndex, out totalItems);
                request.TotalItems = totalItems;
                return new Response<TItem, TKey>
                {
                    Items = items,
                    Request = request
                };
            }

            return new Response<TItem, TKey>
            {
                Item = _repository.Find(request.Id),
                Request = request
            };
        }

        public TKey Post(TItem item)
        {
            return _repository.AddOrUpdate(item);
        }

        public void Delete(TKey itemId)
        {
            _repository.Delete(itemId);
        }
    }
}
