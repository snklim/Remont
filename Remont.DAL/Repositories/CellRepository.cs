using System;
using System.Data.Entity;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL.Repositories
{
    public class CellRepository : TableSpecificRepository<Cell>
    {
        protected override IQueryable<Cell> InternalQuery(PageInfoRequest pageInfoRequest,
            Func<IQueryable<Cell>, IQueryable<Cell>> filter = null)
        {
            var query = base.InternalQuery(pageInfoRequest, filter);

	        query = from column in DbContext.Set<Column>().Where(column => column.TableId == pageInfoRequest.TableId)
		        join cell in query on column.Id equals cell.ColumnId into cells
		        from cell2 in cells.DefaultIfEmpty()
		        join cell3 in DbContext.Set<Cell>() on cell2.Value equals cell3.Id.ToString() into cellDss
		        from cellDs in cellDss.DefaultIfEmpty()
		        select cell2;

			query = query.Include(cell => cell.Column);
			query = query.Include(cell => cell.DataSourceRow);

            return query;
        }

        protected override Cell InternalAddOrUpdate(Cell row)
        {
            row.DataSourceRow = null;
            return base.InternalAddOrUpdate(row);
        }
    }
}