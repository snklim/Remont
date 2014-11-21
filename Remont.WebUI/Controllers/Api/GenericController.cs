using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class GenericController : RemontController<Row>
    {
        private readonly IRepository<Column> _columnRepository;

        public GenericController(IRepository<Row> repository, IRepository<Column> columnRepository) : base(repository)
        {
            _columnRepository = columnRepository;
        }

        public override Response<Row> Get([FromUri]PageInfoRequest pageInfoRequest)
        {
            var response = base.Get(pageInfoRequest);

            response.Bag = _columnRepository.GetAll(pageInfoRequest);

            return response;
        }
    }
}
