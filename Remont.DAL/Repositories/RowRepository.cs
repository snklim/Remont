using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL.Repositories
{
    public class RowRepository : TableSpecificRepository<Row>
    {
        protected override IQueryable<Row> InternalQuery(PageInfoRequest pageInfoRequest, Func<IQueryable<Row>, IQueryable<Row>> filter = null)
        {
            var query = base.InternalQuery(pageInfoRequest, filter);
			
			query = query.Include(row => row.Cells);
			
            return query;
        }

	    protected override IEnumerable<Row> InternalGet(PageInfoRequest pageInfoRequest, Func<IQueryable<Row>, IQueryable<Row>> filter = null)
	    {
		    var rows = base.InternalGet(pageInfoRequest, filter);

			var enumerable = rows as IList<Row> ?? rows.ToList();

		    foreach (var row in enumerable)
		    {
			    var cells =
				    from column in DbContext.Set<Column>().Where(c => c.TableId == pageInfoRequest.TableId)
				    join cell in DbContext.Set<Cell>().Where(c => c.TableId == pageInfoRequest.TableId && c.RowId == row.Id)
					    on column.Id equals cell.ColumnId into cellsLeftJoin
				    from cell2 in cellsLeftJoin.DefaultIfEmpty()
				    select cell2;

			    foreach (var cell in cells.Where(cell => cell == null))
			    {
				    row.Cells.Add(new Cell());
			    }
		    }

		    return enumerable;
	    }
    }
}