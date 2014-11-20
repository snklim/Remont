using System;
using System.Collections.Generic;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL
{
	public class RowRepository : EntityRepository<Row, int>
	{
		public override IList<Row> Get(PageInfoRequest<int> pageInfoRequest, Func<IQueryable<Row>, IQueryable<Row>> filter = null)
		{
			if (filter == null)
				return base.Get(pageInfoRequest, items => items.Where(item => item.TableId == pageInfoRequest.TableId));

			return base.Get(pageInfoRequest, filter);
		}
	}
}
