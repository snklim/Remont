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
                select cell2;

            query = query.Include(cell => cell.Column);

            return query;
        }
    }
}