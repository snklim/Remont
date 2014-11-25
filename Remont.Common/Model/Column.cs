using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class Column : TableSpecificBaseItem
    {
        public string ColumnName { get; set; }

		public Control Control { get; set; }

		public int ControlId { get; set; }

		public int DataSourceTableId { get; set; }

		public int DataSourceColumnId { get; set; }

        public Table Table { get; set; }

        public virtual ICollection<Row> Rows { get; set; }
    }
}
