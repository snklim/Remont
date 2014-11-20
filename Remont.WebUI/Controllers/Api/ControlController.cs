using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class ControlController : RemontController<Control, int>
    {
        public ControlController(IRepository<Control, int> repository) : base(repository)
        {
        }
    }
}
