using System.Collections.Generic;

namespace Remont.Common.Model
{
    public class Table : BaseItem<int>
    {
        public string TableName { get; set; }

        public virtual IEnumerable<Column> Columns { get; set; }

        public virtual IEnumerable<Row> Rows { get; set; }
    }
}
