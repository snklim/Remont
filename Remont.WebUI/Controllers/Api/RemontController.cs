using System.Linq;
using System.Web.Http;
using System.Web.UI;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public abstract class RemontController<TItem> : ApiController
        where TItem : BaseItem
    {
        protected readonly IRepository<TItem> Repository;

        protected RemontController(IRepository<TItem> repository)
        {
            Repository = repository;
        }

        public virtual Response<TItem> Get([FromUri]PageInfoRequest pageInfoRequest)
        {
            if (pageInfoRequest.Id <= 0)
            {
                var items = Repository.Get(pageInfoRequest);

                return new Response<TItem>
                {
                    Items = items.ToList(),
                    PageInfoRequest = pageInfoRequest
                };
            }

            return new Response<TItem>
            {
                Item = Repository.Find(pageInfoRequest),
                PageInfoRequest = pageInfoRequest
            };
        }

		public virtual TItem Post(TItem item)
        {
            return Repository.AddOrUpdate(item);
        }

        public void Delete(int itemId)
        {
            Repository.Delete(itemId);
        }
    }
}
