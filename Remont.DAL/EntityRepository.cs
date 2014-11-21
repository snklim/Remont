using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.DAL
{
    public class EntityRepository<TItem> : IRepository<TItem>, IDisposable
        where TItem : BaseItem
    {
        protected RemontContext DbContext = new RemontContext();

        protected virtual TItem InternalAddOrUpdate(TItem item)
        {
            DbContext.Set<TItem>().AddOrUpdate(item);
            DbContext.SaveChanges();

            return item;
        }

		public TItem AddOrUpdate(TItem item)
		{
		    InternalAddOrUpdate(item);

            return item;
        }

        public void Delete(int itemId)
        {
            var itemDb = DbContext.Set<TItem>().Find(itemId);
            if (itemDb != null)
            {
                DbContext.Set<TItem>().Remove(itemDb);
                DbContext.SaveChanges();
            }
        }

        protected virtual IQueryable<TItem> InternalQuery(PageInfoRequest pageInfoRequest, 
            Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
        {
            var query = DbContext.Set<TItem>().Where(item => !item.IsDeleted);

            if (filter != null)
            {
                query = filter(query);
            }

            return query;
        }

	    private IEnumerable<TItem> InternalGet(PageInfoRequest pageInfoRequest,
		    Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
	    {
		    const int pageSize = 5;

            var query = InternalQuery(pageInfoRequest, filter);

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

	    public IEnumerable<TItem> Get(PageInfoRequest pageInfoRequest,
		    Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
	    {
		    return InternalGet(pageInfoRequest, filter).ToList();
	    }

        protected virtual TItem InternalFind(PageInfoRequest pageInfoRequest)
        {
            return InternalQuery(pageInfoRequest).FirstOrDefault(item => item.Id == pageInfoRequest.Id);
        }

        public TItem Find(PageInfoRequest pageInfoRequest)
        {
            return InternalFind(pageInfoRequest);
        }

        public IQueryable<TItem> GetAll(PageInfoRequest pageInfoRequest = null, 
            Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
        {
            return InternalQuery(pageInfoRequest, filter);
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