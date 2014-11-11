using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class Order : BaseItem<int>
    {
        public virtual DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual int OrderStatusId { get; set; }
    }
}
