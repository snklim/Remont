using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class OrderStatus : BaseItem<int>
    {
        public string StatusName { get; set; }
    }
}
