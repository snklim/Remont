using System;
using System.Data.Entity;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL
{
	public class RowRepository : TableSpecificRepository<Row>
	{
		protected override IQueryable<Row> InternalGet(PageInfoRequest<int> pageInfoRequest, Func<IQueryable<Row>, IQueryable<Row>> filter = null)
		{
			var query = base.InternalGet(pageInfoRequest, filter);

			var rowCells = query.Join(DbContext.Set<Cell>(), r => r.Id, c => c.RowId, (r, c) => new {Row = r, Cell = c});

			var rr = from column in DbContext.Set<Column>().Where(c => c.TableId == pageInfoRequest.TableId)
				join rowCell in rowCells on column.Id equals rowCell.Cell.ColumnId into rowCellJoins
				from rowCellJoin in rowCellJoins.DefaultIfEmpty()
				select new {column, rowCellJoin};

			//query = query
			//	.Join(DbContext.Set<Cell>(), r => r.Id, c => c.RowId, (r, c) => new {Row = r, Cell = c})
			//	.Join(DbContext.Set<Column>(), rowCell => rowCell.Cell.ColumnId, column => column.Id,
			//		(rowCell, column) => new {rowCell.Row, rowCell.Cell, Column = column})
			//	.Select(rowCellColumn => rowCellColumn.Row)
			//	.Distinct();

			return query;
		}
	}
}
