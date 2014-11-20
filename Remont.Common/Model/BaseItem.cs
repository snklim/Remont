
using System.Collections.Generic;

namespace Remont.Common.Model
{
    public class BaseItem<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual bool IsDeleted { get; set; }
    }

	public class TableBaseItem<TKey> : BaseItem<TKey>
		where TKey : struct
	{
		public virtual TKey TableId { get; set; }
	}
}
