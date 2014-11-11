using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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

        public IList<TItem> Get(int pageIndex, out int totalItems, out int totalPages, out int pageIndexOut)
        {
            const int pageSize = 2;

            totalItems = _db.Set<TItem>().Count();
            totalPages = totalItems/pageSize + (totalItems%pageSize == 0 ? 0 : 1);
            pageIndexOut = pageIndex;

            if (pageIndex < 0)
            {
                pageIndexOut = 0;
            }
            else if (pageIndex >= totalPages)
            {
                pageIndexOut = totalPages - 1;
            }
            
            return _db.Set<TItem>()
                .OrderBy(item => item.Id)
                .Skip(pageIndexOut * pageSize)
                .Take(pageSize)
                .ToArray();
        }

        public TItem Find(TKey itemId)
        {
            return _db.Set<TItem>().Find(itemId);
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