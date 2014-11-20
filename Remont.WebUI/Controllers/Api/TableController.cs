using System.Linq;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class TableController : RemontController<Table, int>
    {
        private readonly IRepository<Column, int> _columnRepository;

        public TableController(IRepository<Table, int> tableTepository, IRepository<Column, int> columnRepository)
            : base(tableTepository)
        {
            _columnRepository = columnRepository;
        }

		public override Table Post(Table item)
        {
			item = base.Post(item);
            
            if (item.Columns != null)
            {
                item.Columns.ForEach(c =>
                {
                    c.TableId = item.Id;
                    c.Id = _columnRepository.AddOrUpdate(c).Id;
                });
            }

			return item;
        }

        public override Response<Table, int> Get([FromUri]PageInfoRequest<int> pageInfoRequest)
        {
            var response = base.Get(pageInfoRequest);

            if (response.Item != null)
            {
                response.Item.Columns =
                    _columnRepository.GetAll(query => query.Where(c => c.TableId == response.Item.Id));
            }

            return response;
        }
    }
}
