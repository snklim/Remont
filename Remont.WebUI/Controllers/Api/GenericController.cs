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

        //[Route("api/generic/{tableId:int}/{rowId:int?}")]
        public Response<Row, int> Get([FromUri]PageInfoRequest<int> pageInfoRequest)
        {
            var tableId = pageInfoRequest.TableId;
            var rowId = -1;//pageInfoRequest.RowId;

            var table = _tableRepository.Find(tableId);

            if (table == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

			table.Columns = _columnRepository.GetAll(items => items.Where(item => item.TableId == tableId));

            //if (rowId == -1)
            //{
            //    table.Rows = _rowRepository.GetAll(items => items
            //        .Where(item => item.TableId == tableId)
            //        /*.OrderBy(item => item.Id).Skip(5).Take(5)*/);
            //    table.Rows.ForEach(r => r.Cells = _cellRepository.GetAll(items => items.Where(item => item.RowId == r.Id)));
            //}
            //else if (rowId > 0)
            //{
            //    table.Rows = _rowRepository.GetAll(items => items.Where(item => item.Id == rowId));
            //    table.Rows.ForEach(r => r.Cells = _cellRepository
            //        .GetAll(items => items.Where(item => item.RowId == r.Id))
            //        .Select(c =>
            //        {
            //            c.Column = _columnRepository.GetAll(items => items.Where(item => item.Id == c.ColumnId)).FirstOrDefault();
            //            return c;
            //        }));
            //}
            //else if (rowId == 0)
            //{
            //    table.Rows = new[]
            //    {
            //        new Row
            //        {
            //            TableId = tableId,
            //            Cells = table.Columns.Select(c => new Cell
            //            {
            //                ColumnId = c.Id,
            //                Column = c,
            //                TableId = tableId
            //            })
            //        }
            //    };
            //}

            table.Rows = _rowRepository.Get(pageInfoRequest);
            table.Rows.ForEach(r => r.Cells = _cellRepository.GetAll(items => items.Where(item => item.RowId == r.Id)));

	        return new Response<Row, int>
            {
				Bag = table.Columns,
                Items = table.Rows.ToList(),
                PageInfoRequest = pageInfoRequest
            };
        }

        [Route("api/generic/{tableId:int}")]
        public int Post([FromUri]int tableId, Row row)
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
