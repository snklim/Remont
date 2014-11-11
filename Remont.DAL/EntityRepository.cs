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

        public IList<TItem> Get(int pageIndex, out int totalItems)
        {
            const int pageSize = 5;

            totalItems = _db.Set<TItem>().Count();

            return _db.Set<TItem>()
                .OrderBy(item => item.Id)
                .Skip(pageIndex*pageSize)
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