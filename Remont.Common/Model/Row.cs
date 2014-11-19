using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class Row : BaseItem<int>
    {
        public Table Table { get; set; }

        public Column Column { get; set; }

        public int TableId { get; set; }

        public int ColumnId { get; set; }

        public int RecordId { get; set; }

        public string Value { get; set; }
    }
}
