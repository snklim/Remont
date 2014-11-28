using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            query = query
                .Include(row => row.Cells)
				.Include("Cells.DataSourceRow")
				.Include("Cells.DataSourceRow.Table")
                .Include("Cells.DataSourceRow.Cells");
			
            return query;
        }

	    protected override IEnumerable<Row> InternalGet(PageInfoRequest pageInfoRequest, Func<IQueryable<Row>, IQueryable<Row>> filter = null)
	    {
		    var rows = base.InternalGet(pageInfoRequest, filter).ToList();

		    var columns = DbContext.Set<Column>().Where(column => column.TableId == pageInfoRequest.TableId).ToList();
				
		    foreach (var row in rows)
		    {
			    MapCellAndColumn(pageInfoRequest, row, columns);
		    }

		    return rows;
	    }

	    protected override Row InternalFind(PageInfoRequest pageInfoRequest)
	    {
		    var row = base.InternalFind(pageInfoRequest);
			var columns = DbContext.Set<Column>().Where(column => column.TableId == pageInfoRequest.TableId).ToList();

		    if (row == null)
		    {
			    row = new Row
			    {
				    TableId = pageInfoRequest.TableId,
					Cells = new Collection<Cell>()
			    };
		    }

			MapCellAndColumn(pageInfoRequest, row, columns);

		    return row;
	    }

        protected override Row InternalAddOrUpdate(Row item)
        {
            foreach (var cell in item.Cells)
            {
                cell.Column = null;
                cell.DataSourceRow = null;
            }
            return base.InternalAddOrUpdate(item);
        }

        private void MapCellAndColumn(PageInfoRequest pageInfoRequest, Row row, List<Column> columns)
	    {
		    var cells =
			    from column in DbContext.Set<Column>().Where(c => c.TableId == pageInfoRequest.TableId)
			    join cell in DbContext.Set<Cell>().Where(c => c.TableId == pageInfoRequest.TableId && c.RowId == row.Id)
				    on column.Id equals cell.ColumnId into cellsLeftJoin
			    from cell2 in cellsLeftJoin.DefaultIfEmpty()
			    select cell2;

		    int columnIndex = 0;
		    foreach (var cell in cells)
		    {
			    if (cell == null)
			    {
				    row.Cells.Add(new Cell
				    {
						RowId = row.Id,
					    TableId = pageInfoRequest.TableId,
					    ColumnId = columns[columnIndex].Id
				    });
			    }
			    columnIndex++;
		    }
	    }
    }
}