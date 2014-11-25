using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
	public class DataSourceRequest : PageInfoRequest
	{
		public int ColumnId { get; set; }
	}

	public class DataSourceController : RemontController<Cell, DataSourceRequest>
    {
	    public DataSourceController(IRepository<Cell> repository) : base(repository)
	    {
	    }

		public override Response<Cell> Get([FromUri]DataSourceRequest pageInfoRequest)
	    {
			return new Response<Cell>
			{
				Items = Repository.GetAll(pageInfoRequest, cells => cells.Where(cell => cell.ColumnId == pageInfoRequest.ColumnId)).ToList(),
				PageInfoRequest = pageInfoRequest
			};
	    }
    }
}
