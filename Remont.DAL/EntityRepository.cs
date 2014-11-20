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
        private RemontContext _db = new RemontContext();

        public TKey AddOrUpdate(TItem item)
        {
            _db.Set<TItem>().AddOrUpdate(item);
            _db.SaveChanges();

            return item.Id;
        }

        public void Delete(TKey itemId)
        {
            var itemDb = _db.Set<TItem>().Find(itemId);
            if (itemDb != null)
            {
                _db.Set<TItem>().Remove(itemDb);
                _db.SaveChanges();
            }
        }

        public IList<TItem> Get(PageInfoRequest<TKey> pageInfoRequest)
        {
            const int pageSize = 5;

            pageInfoRequest.TotalItems = _db.Set<TItem>().Count();
            pageInfoRequest.TotalPages = pageInfoRequest.TotalItems / pageSize + (pageInfoRequest.TotalItems % pageSize == 0 ? 0 : 1);

            if (pageInfoRequest.PageIndex < 0)
            {
                pageInfoRequest.PageIndex = 0;
            }
            else if (pageInfoRequest.PageIndex > 0 && pageInfoRequest.PageIndex >= pageInfoRequest.TotalPages)
            {
                pageInfoRequest.PageIndex = pageInfoRequest.TotalPages - 1;
            }

            var query = _db.Set<TItem>()
                .OrderBy(item => item.Id)
                .Skip(pageInfoRequest.PageIndex * pageSize)
                .Take(pageSize);

            return query.ToList();
        }

        public TItem Find(TKey itemId)
        {
            return _db.Set<TItem>().Find(itemId);
        }

        public IList<TItem> GetAll(Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
        {
            var query = _db.Set<TItem>().Where(item => !item.IsDeleted);
            return filter != null ? filter(query).ToList() : query.ToList();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }
}