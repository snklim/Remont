using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.DAL
{
    public class EntityRepository<TItem, TKey> : IRepository<TItem, TKey>, IDisposable
        where TItem : BaseItem<TKey>
    {
        protected RemontContext DbContext = new RemontContext();

		public TItem AddOrUpdate(TItem item)
        {
            DbContext.Set<TItem>().AddOrUpdate(item);
            DbContext.SaveChanges();

            return item;
        }

        public void Delete(TKey itemId)
        {
            var itemDb = DbContext.Set<TItem>().Find(itemId);
            if (itemDb != null)
            {
                DbContext.Set<TItem>().Remove(itemDb);
                DbContext.SaveChanges();
            }
        }

	    protected virtual IQueryable<TItem> InternalGet(PageInfoRequest<TKey> pageInfoRequest,
		    Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
	    {
		    const int pageSize = 5;

		    var query = DbContext.Set<TItem>().Where(item => !item.IsDeleted);

		    if (filter != null)
		    {
			    query = filter(query);
		    }

		    pageInfoRequest.TotalItems = query.Count();
		    pageInfoRequest.TotalPages = pageInfoRequest.TotalItems/pageSize +
		                                 (pageInfoRequest.TotalItems%pageSize == 0 ? 0 : 1);

		    if (pageInfoRequest.PageIndex < 0)
		    {
			    pageInfoRequest.PageIndex = 0;
		    }
		    else if (pageInfoRequest.PageIndex > 0 && pageInfoRequest.PageIndex >= pageInfoRequest.TotalPages)
		    {
			    pageInfoRequest.PageIndex = pageInfoRequest.TotalPages - 1;
		    }

		    query = query.OrderBy(item => item.Id)
			    .Skip(pageInfoRequest.PageIndex*pageSize)
			    .Take(pageSize);

		    return query;
	    }

	    public IEnumerable<TItem> Get(PageInfoRequest<TKey> pageInfoRequest,
		    Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
	    {
		    return InternalGet(pageInfoRequest, filter).ToList();
	    }

        public TItem Find(TKey itemId)
        {
            return DbContext.Set<TItem>().Find(itemId);
        }

        public IEnumerable<TItem> GetAll(Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
        {
            var query = DbContext.Set<TItem>().Where(item => !item.IsDeleted);
            return filter != null ? filter(query).ToList() : query.ToList();
        }

        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
                DbContext = null;
            }
        }
    }
}