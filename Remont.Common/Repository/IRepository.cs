using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common.Model;

namespace Remont.Common.Repository
{
    public interface IRepository<TItem> : IDisposable
        where TItem : BaseItem
    {
		TItem AddOrUpdate(TItem item);

        void Delete(int itemId);

		IEnumerable<TItem> Get(PageInfoRequest pageInfoRequest, Func<IQueryable<TItem>, IQueryable<TItem>> filter = null);

        TItem Find(PageInfoRequest pageInfoRequest);

        TItem Delete(TItem item);

        IQueryable<TItem> GetAll(PageInfoRequest pageInfoRequest = null,
            Func<IQueryable<TItem>, IQueryable<TItem>> filter = null);
    }
}
