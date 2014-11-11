using System.Collections.Generic;
using Remont.Common.Model;

namespace Remont.Common.Repository
{
    public interface IRepository<TItem, TKey> where TItem : BaseItem<TKey>
    {
        TKey AddOrUpdate(TItem item);

        void Delete(TKey itemId);

        IList<TItem> Get(int pageIndex, out int totalItems, out int totalPages, out int pageIndexOut);

        TItem Find(TKey itemId);
    }
}
