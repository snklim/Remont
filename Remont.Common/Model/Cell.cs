
using System.Collections.Generic;

namespace Remont.Common.Model
{
	public class Cell : TableSpecificBaseItem
	{
		public virtual Row Row { get; set; }

		public virtual int RowId { get; set; }

		public virtual Column Column { get; set; }

		public virtual int ColumnId { get; set; }

		public virtual Table Table { get; set; }

		public virtual string Value { get; set; }
		
        public Row DataSourceRow { get; set; }

        public int? DataSourceRowId { get; set; }

		public ICollection<Row> DataSourceRows { get;set; }
	}
}
