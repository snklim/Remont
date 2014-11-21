using System.Collections.Generic;

namespace Remont.Common.Model
{
    public class Table : BaseItem
    {
        public string TableName { get; set; }

        public virtual ICollection<Column> Columns { get; set; }

        public virtual ICollection<Row> Rows { get; set; }
    }
}
