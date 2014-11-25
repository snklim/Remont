using System.Data.Entity.Migrations;
using System.Linq;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.DAL.Repositories
{
	public class TableRepository : EntityRepository<Table>
    {
        protected override Table InternalFind(PageInfoRequest pageInfoRequest)
        {
            var table = base.InternalFind(pageInfoRequest);

	        if (table != null)
	        {
		        table.Columns = DbContext.Set<Column>().Where(c => c.TableId == pageInfoRequest.Id).ToList();
	        }

	        return table;
        }

        protected override Table InternalAddOrUpdate(Table item)
        {
            item = base.InternalAddOrUpdate(item);

            foreach (var c in item.Columns)
            {
                c.TableId = item.Id;
                DbContext.Set<Column>().AddOrUpdate(c);
            }
            DbContext.SaveChanges();

            return item;
        }
    }
}