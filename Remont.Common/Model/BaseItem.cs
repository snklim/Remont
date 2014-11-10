using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remont.Common.Model
{
    public class BaseItem<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
