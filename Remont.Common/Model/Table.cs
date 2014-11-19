using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class Table : BaseItem<int>
    {
        public string TableName { get; set; }

        public virtual IEnumerable<Column> Columns { get; set; }

        public virtual IEnumerable<Row> Rows { get; set; }
    }
}
