using System.Collections.Generic;

namespace Remont.Common.Model
{
    public class Row : BaseItem<int>
    {
        public Table Table { get; set; }

        public int TableId { get; set; }

		public IEnumerable<Cell> Cells { get; set; }
    }
}
