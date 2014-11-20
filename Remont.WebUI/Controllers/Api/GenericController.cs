using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class GenericController : ApiController
    {
        private readonly IRepository<Table, int> _tableRepository;
        private readonly IRepository<Column, int> _columnRepository;
		private readonly IRepository<Row, int> _rowRepository;
		private readonly IRepository<Cell, int> _cellRepository;

        public GenericController(IRepository<Table, int> tableRepository, 
            IRepository<Column, int> columnRepository,
            IRepository<Row, int> rowRepository, IRepository<Cell, int> cellRepository)
        {
            _tableRepository = tableRepository;
            _columnRepository = columnRepository;
            _rowRepository = rowRepository;
	        _cellRepository = cellRepository;
        }

        public Response<Row, int> Get([FromUri]PageInfoRequest<int> pageInfoRequest)
        {
            var tableId = pageInfoRequest.TableId;

            var table = _tableRepository.Find(tableId);

            if (table == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

			table.Columns = _columnRepository.GetAll(items => items.Where(item => item.TableId == tableId));
			
            table.Rows = _rowRepository.Get(pageInfoRequest);
	        table.Rows.ForEach(
		        r => r.Cells = _cellRepository
			        .GetAll(items => items.Where(item => item.TableId == table.Id && item.RowId == r.Id)));

	        var row = new Row
	        {
				TableId = tableId,
				Cells = table.Columns.Select(column => new Cell
				{
					Column = column,
					ColumnId = column.Id,
					TableId = tableId
				})
	        };

	        if (pageInfoRequest.Id > 0)
	        {
		        row = _rowRepository.Find(pageInfoRequest.Id);

		        if (row == null)
		        {
					throw new HttpResponseException(HttpStatusCode.NotFound);
		        }

		        row.Cells = _cellRepository.GetAll(items => items.Where(item => item.RowId == row.Id)).Select(item =>
		        {
			        item.Column = _columnRepository.Find(item.ColumnId);
			        return item;
		        });
	        }

	        return new Response<Row, int>
            {
				Bag = table.Columns,
				Item = row,
                Items = table.Rows.ToList(),
                PageInfoRequest = pageInfoRequest
            };
        }

        public int Post(Row row)
        {
	        _rowRepository.AddOrUpdate(row);

	        row.Cells.ForEach(c =>
	        {
		        c.RowId = row.Id;
		        c.Column = null;
		        _cellRepository.AddOrUpdate(c);
	        });

			return row.Id;
        }
    }
}
