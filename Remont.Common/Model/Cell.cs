
namespace Remont.Common.Model
{
	public class Cell : TableSpecificBaseItem
	{
		public virtual Row Row { get; set; }

		public virtual int RowId { get; set; }

		public virtual Column Column { get; set; }

		public virtual int ColumnId { get; set; }

		public virtual Table Table { get; set; }

		public virtual string Value { get; set; }

		public Cell CellDataSource { get; set; }

		public int? CellDataSourceId { get; set; }
	}
}
