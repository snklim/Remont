using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common.Model;

namespace Remont.Common.Repository
{
    public interface IRepository<TItem, TKey> where TItem : BaseItem<TKey>
    {
        TKey AddOrUpdate(TItem item);

        void Delete(TKey itemId);

        IList<TItem> Get(int pageIndex, out int totalItems, out int totalPages, out int pageIndexOut);

        TItem Find(TKey itemId);

        IList<TItem> GetAll(Func<IQueryable<TItem>, IQueryable<TItem>> filter = null);
    }
}
