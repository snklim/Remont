using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Remont.Common.Model
{
    public class Table : BaseItem
    {
        public string TableName { get; set; }

        private ICollection<Column> _columns;
        public virtual ICollection<Column> Columns { get { return _columns ?? (_columns = new Collection<Column>()); } }

        public virtual ICollection<Row> Rows { get; set; }
    }
}
