using System.Collections.Generic;

namespace Remont.Common.Model
{
	public class Row : TableBaseItem<int>
    {
        public Table Table { get; set; }

		public IEnumerable<Cell> Cells { get; set; }
    }
}
