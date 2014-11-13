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

        public virtual IList<Column> Columns { get; set; }
    }
}
