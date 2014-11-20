using System.Collections.Generic;

namespace Remont.Common.Model
{
	public class Row : TableSpecificBaseItem<int>
    {
        public Table Table { get; set; }

		public IEnumerable<Cell> Cells { get; set; }
    }
}
