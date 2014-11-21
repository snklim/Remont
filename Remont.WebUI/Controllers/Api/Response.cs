using System.Collections.Generic;
using Remont.Common;

namespace Remont.WebUI.Controllers.Api
{
    public class Response<TItem>
    {
        public IList<TItem> Items { get; set; }

        public TItem Item { get; set; }

        public PageInfoRequest PageInfoRequest { get; set; }

        public object Bag { get; set; }
    }
}