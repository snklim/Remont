using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL.Repositories
{
	public class TableRepository : EntityRepository<Table>
    {
	    protected override IQueryable<Table> InternalQuery(PageInfoRequest pageInfoRequest, Func<IQueryable<Table>, IQueryable<Table>> filter = null)
	    {
	        return base.InternalQuery(pageInfoRequest, filter).Include(t => t.Columns);
	    }

        protected override Table InternalAddOrUpdate(Table row)
        {
            row = base.InternalAddOrUpdate(row);

            foreach (var c in row.Columns)
            {
                c.TableId = row.Id;
                DbContext.Set<Column>().AddOrUpdate(c);
            }
            DbContext.SaveChanges();

            return row;
        }
    }
}