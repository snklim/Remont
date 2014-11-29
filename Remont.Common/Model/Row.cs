using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Remont.Common.Model
{
	public class Row : TableSpecificBaseItem
    {
		public virtual Table Table { get; set; }

	    private ICollection<Cell> _cells;

	    public virtual ICollection<Cell> Cells
	    {
	        get { return _cells ?? (_cells = new Collection<Cell>()); }
	    }
    }
}
