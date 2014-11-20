
namespace Remont.Common.Model
{
	public class Cell : TableSpecificBaseItem<int>
	{
		public Row Row { get; set; }

		public int RowId { get; set; }

		public Column Column { get; set; }

		public int ColumnId { get; set; }

		public Table Table { get; set; }

		public string Value { get; set; }
	}
}
