using System.Collections.Generic;

namespace Remont.Common.Model
{
	public class Row : TableSpecificBaseItem
    {
		public virtual Table Table { get; set; }

        public virtual ICollection<Cell> Cells { get; set; }

        public virtual ICollection<Cell> DataSourceCells { get; set; }
    }
}
