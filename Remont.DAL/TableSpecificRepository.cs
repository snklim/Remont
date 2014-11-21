using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL
{
	public class TableSpecificRepository<TItem> : EntityRepository<TItem, int>
		where TItem : TableSpecificBaseItem<int>
	{
		protected override IQueryable<TItem> InternalGet(PageInfoRequest<int> pageInfoRequest, Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
		{
			var baseFilter = new Func<IQueryable<TItem>, IQueryable<TItem>>(
				items => items.Where(item => item.TableId == pageInfoRequest.TableId));

			if (filter == null)
				return base.InternalGet(pageInfoRequest, baseFilter);

			return base.InternalGet(pageInfoRequest, items => filter(baseFilter(items)));
		}
	}
}
