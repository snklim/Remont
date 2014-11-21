
namespace Remont.Common.Model
{
    public class BaseItem
    {
        public virtual int Id { get; set; }

        public virtual bool IsDeleted { get; set; }
    }

	public class TableSpecificBaseItem : BaseItem
	{
		public virtual int TableId { get; set; }
	}
}
