using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL
{
	public class TableSpecificRepository<TItem> : EntityRepository<TItem, int>
		where TItem : TableBaseItem<int>
	{
		public override IList<TItem> Get(PageInfoRequest<int> pageInfoRequest, Func<IQueryable<TItem>, IQueryable<TItem>> filter = null)
		{
			var baseFilter = new Func<IQueryable<TItem>, IQueryable<TItem>>(
				items => items.Where(item => item.TableId == pageInfoRequest.TableId));

			if (filter == null)
				return base.Get(pageInfoRequest, baseFilter);

			return base.Get(pageInfoRequest, items => filter(baseFilter(items)));
		}
	}
}
