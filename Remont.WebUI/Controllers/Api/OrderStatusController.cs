using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Remont.Common.Model;
using Remont.Common.Repository;
using Remont.DAL;

namespace Remont.WebUI.Controllers.Api
{
    public class OrderStatusController : RemontController<OrderStatus, int>
    {
        public OrderStatusController(IRepository<OrderStatus, int> repository)
            : base(repository)
        {
        }
    }
}
