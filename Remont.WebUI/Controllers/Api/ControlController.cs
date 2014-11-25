using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
	public class ControlController : RemontController<Control, PageInfoRequest>
    {
        public ControlController(IRepository<Control> repository) : base(repository)
        {
        }
    }
}
