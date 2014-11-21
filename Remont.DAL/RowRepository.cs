using System;
using System.Data.Entity;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL
{
    public class RowRepository : TableSpecificRepository<Row>
    {
        protected override IQueryable<Row> InternalQuery(PageInfoRequest pageInfoRequest, Func<IQueryable<Row>, IQueryable<Row>> filter = null)
        {
            var query = base.InternalQuery(pageInfoRequest, filter);

            query = query.Include(row => row.Cells);

            return query;
        }
    }
}