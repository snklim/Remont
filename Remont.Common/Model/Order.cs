using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public enum OrderStatus
    {
        New,
        InProgress,
        Completed
    }

    public class Order : BaseItem<int>
    {
        public virtual DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
    }
}
