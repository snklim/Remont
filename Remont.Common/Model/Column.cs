using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class Column : BaseItem<int>
    {
        public string ColumnName { get; set; }

        public int TableId { get; set; }

        public Table Table { get; set; }

        public virtual IEnumerable<Row> Rows { get; set; }
    }
}
