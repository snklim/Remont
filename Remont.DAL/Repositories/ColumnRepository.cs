using System;
using System.Data.Entity;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL.Repositories
{
	public class ColumnRepository : TableSpecificRepository<Column>
	{
		protected override IQueryable<Column> InternalQuery(PageInfoRequest pageInfoRequest, Func<IQueryable<Column>, IQueryable<Column>> filter = null)
		{
			var query = base.InternalQuery(pageInfoRequest, filter);

			query = query.Include(column => column.Control);
			query = query.Include(column => column.DataSourceTable);

			return query;
		}

		protected override Column InternalAddOrUpdate(Column item)
		{
			item.DataSourceTable = null;
			return base.InternalAddOrUpdate(item);
		}
	}
}