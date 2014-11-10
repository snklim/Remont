using System.Collections.Generic;
using System.Web.Http;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class CustomerController : RemontController<Customer, int>
    {
        public CustomerController(IRepository<Customer, int> repository) : base(repository)
        {
        }
    }
}
