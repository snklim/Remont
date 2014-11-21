using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class GenericController : RemontController<Row, int>
    {
        private readonly IRepository<Table, int> _tableRepository;
        private readonly IRepository<Column, int> _columnRepository;
		private readonly IRepository<Cell, int> _cellRepository;

        public GenericController(IRepository<Table, int> tableRepository, 
            IRepository<Column, int> columnRepository,
            IRepository<Row, int> rowRepository, IRepository<Cell, int> cellRepository)
            : base(rowRepository)
        {
            _tableRepository = tableRepository;
            _columnRepository = columnRepository;
	        _cellRepository = cellRepository;
        }

        public override Response<Row, int> Get([FromUri]PageInfoRequest<int> pageInfoRequest)
        {
            var tableId = pageInfoRequest.TableId;

            var table = _tableRepository.Find(tableId);

            if (table == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

			table.Columns = _columnRepository.GetAll(items => items.Where(item => item.TableId == tableId));

	        table.Rows = Repository.Get(pageInfoRequest);

			if (table.Rows == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			Row row;

	        if (pageInfoRequest.Id > 0)
	        {
		        row = Repository.Find(pageInfoRequest.Id);

		        if (row == null)
		        {
			        throw new HttpResponseException(HttpStatusCode.NotFound);
		        }
	        }
	        else
	        {
				row = new Row
				{
					TableId = tableId,
					Cells = table.Columns.Select(column => new Cell
					{
						Column = column,
						ColumnId = column.Id,
						TableId = tableId
					}).ToList()
				};
	        }

	        return new Response<Row, int>
            {
				Bag = table.Columns,
				Item = row,
                Items = table.Rows.ToList(),
                PageInfoRequest = pageInfoRequest
            };
        }

        public override Row Post(Row row)
        {
            Repository.AddOrUpdate(row);

	        row.Cells.ForEach(c =>
	        {
		        c.RowId = row.Id;
		        c.Column = null;
		        _cellRepository.AddOrUpdate(c);
	        });

			return row;
        }
    }
}
