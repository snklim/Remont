using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common.Model;

namespace Remont.Common.Repository
{
    public interface IRepository<TItem, TKey> where TItem : BaseItem<TKey>
    {
		TItem AddOrUpdate(TItem item);

        void Delete(TKey itemId);

		IEnumerable<TItem> Get(PageInfoRequest<TKey> pageInfoRequest, Func<IQueryable<TItem>, IQueryable<TItem>> filter = null);

        TItem Find(TKey itemId);

		IEnumerable<TItem> GetAll(Func<IQueryable<TItem>, IQueryable<TItem>> filter = null);
    }
}
