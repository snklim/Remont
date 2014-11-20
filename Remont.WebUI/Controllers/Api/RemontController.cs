﻿using System.Linq;
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
        private readonly IRepository<TItem, TKey> _repository;

        protected RemontController(IRepository<TItem, TKey> repository)
        {
            _repository = repository;
        }

        public virtual Response<TItem, TKey> Get([FromUri]PageInfoRequest<TKey> pageInfoRequest)
        {
            if (pageInfoRequest.Id.Equals(default(TKey)))
            {
                var items = _repository.Get(pageInfoRequest);

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

		public virtual TItem Post(TItem item)
        {
            return _repository.AddOrUpdate(item);
        }

        public void Delete(TKey itemId)
        {
            _repository.Delete(itemId);
        }
    }
}
