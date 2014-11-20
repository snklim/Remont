using System.Linq;
using System.Web.Http;
using System.Web.UI;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public abstract class RemontController<TItem, TKey> : ApiController
        where TItem : BaseItem<TKey>
    {
        protected readonly IRepository<TItem, TKey> Repository;

        protected RemontController(IRepository<TItem, TKey> repository)
        {
            Repository = repository;
        }

        public virtual Response<TItem, TKey> Get([FromUri]PageInfoRequest<TKey> pageInfoRequest)
        {
            if (pageInfoRequest.Id.Equals(default(TKey)))
            {
                var items = Repository.Get(pageInfoRequest);

                return new Response<TItem, TKey>
                {
                    Items = items,
                    PageInfoRequest = pageInfoRequest
                };
            }

            return new Response<TItem, TKey>
            {
                Item = Repository.Find(pageInfoRequest.Id),
                PageInfoRequest = pageInfoRequest
            };
        }

		public virtual TItem Post(TItem item)
        {
            return Repository.AddOrUpdate(item);
        }

        public void Delete(TKey itemId)
        {
            Repository.Delete(itemId);
        }
    }
}
