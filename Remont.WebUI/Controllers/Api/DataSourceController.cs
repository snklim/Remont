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
	public class DataSourceController : RemontController<Row, PageInfoRequest>
    {
		public DataSourceController(IRepository<Row> repository)
			: base(repository)
	    {
	    }

		public override Response<Row> Get([FromUri]PageInfoRequest pageInfoRequest)
	    {
			return new Response<Row>
			{
				Items = Repository.GetAll(pageInfoRequest, rows => rows.Where(row => row.TableId == pageInfoRequest.TableId)).ToList(),
				PageInfoRequest = pageInfoRequest
			};
	    }
    }
}
