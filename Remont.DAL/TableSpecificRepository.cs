using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL
{
	public class TableSpecificRepository<TItem> : EntityRepository<TItem>
		where TItem : TableSpecificBaseItem
	{
        protected override IQueryable<TItem> InternalQuery(PageInfoRequest pageInfoRequest, 
            Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
		{
            if (pageInfoRequest == null)
            {
                throw new ArgumentNullException("pageInfoRequest");
            }

			var baseFilter = new Func<IQueryable<TItem>, IQueryable<TItem>>(
				items => items.Where(item => item.TableId == pageInfoRequest.TableId));

			if (filter == null)
                return base.InternalQuery(pageInfoRequest, baseFilter);

            return base.InternalQuery(pageInfoRequest, items => filter(baseFilter(items)));
		}
	}
}
