using System;
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

            //query = query.Include(row => row.Cells);

            //query = from row in query
            //    join cell in DbContext.Set<Cell>() on row.Id equals cell.RowId
            //    select row;

/*
select * from 
(
	select c1.Id as ColumnId, r.Id as RowId from [Column] c1, [Row] r
	where r.TableId = 1 and c1.TableId = 1
) as tt
left join Cell c on tt.ColumnId = c.ColumnId and tt.RowId = c.RowId
*/

            return query;
        }
    }
}