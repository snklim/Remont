using System;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL.Repositories
{
    public class TableSpecificRepository<TItem> : EntityRepository<TItem>
		where TItem : TableSpecificBaseItem
	{
        protected override IQueryable<TItem> InternalQuery(PageInfoRequest pageInfoRequest, 
            Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
		{
            var baseFilter =  new Func<IQueryable<TItem>, IQueryable<TItem>>(items => items);

            if (pageInfoRequest != null && pageInfoRequest.TableId > 0)
            {
                baseFilter = items => items.Where(item => item.TableId == pageInfoRequest.TableId);
            }

            if (filter == null)
                return base.InternalQuery(pageInfoRequest, baseFilter);

            return base.InternalQuery(pageInfoRequest, items => filter(baseFilter(items)));
		}
	}
}
