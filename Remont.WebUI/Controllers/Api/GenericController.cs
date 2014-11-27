using System.Linq;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
	public class GenericController : RemontController<Row, PageInfoRequest>
    {
        private readonly IRepository<Column> _columnRepository;
        private readonly IRepository<Cell> _cellRepository;

        public GenericController(
            IRepository<Row> repository, 
            IRepository<Column> columnRepository, 
            IRepository<Cell> cellRepository) : base(repository)
        {
            _columnRepository = columnRepository;
            _cellRepository = cellRepository;
        }

        public override Response<Row> Get([FromUri]PageInfoRequest pageInfoRequest)
        {
            var response = base.Get(pageInfoRequest);

            var columns = _columnRepository.GetAll(pageInfoRequest).ToList();

            response.Bag = columns;

            if (response.Items != null)
            {
				//response.Items.ForEach(row => _cellRepository
				//	.GetAll(pageInfoRequest, cells => cells
				//		.Where(c => c.RowId == row.Id)).ToList()
				//	.ForEach(c => row.Cells.Add(c != null
				//		? new Cell
				//		{
				//			Id = c.Id,
				//			Value = c.DataSourceCell != null ? c.DataSourceCell.Value : c.Value
				//		}
				//		: new Cell())));
            }
            else if (response.Item != null)
            {
				//var row = response.Item;
				//_cellRepository
				//	.GetAll(pageInfoRequest, cells => cells
				//		.Where(c => c.RowId == row.Id)).ToList()
				//	.ForEach(
				//		c => row.Cells.Add(c != null
				//			? new Cell
				//			{
				//				Id = c.Id,
				//				RowId = c.RowId,
				//				ColumnId = c.ColumnId,
				//				DataSourceCellId = c.DataSourceCellId,
				//				DataSourceRowId = c.DataSourceRowId,
				//				TableId = pageInfoRequest.TableId,
				//				Value = c.Value,
				//				//DataSourceRow = c.DataSourceRow
				//			}
				//			: new Cell
				//			{
				//				ColumnId = columns[row.Cells.Count].Id,
				//				TableId = pageInfoRequest.TableId
				//			}));
            }
            else
            {
				//response.Item = new Row
				//{
				//	TableId = pageInfoRequest.TableId,
				//	Cells = columns.Select(column => new Cell
				//	{
				//		ColumnId = column.Id,
				//		TableId = pageInfoRequest.TableId
				//	}).ToList()
				//};
            }

            return response;
        }

        public override Row Post(Row item)
        {
            item = base.Post(item);

            item.Cells.ForEach(c => _cellRepository.AddOrUpdate(c));

            return item;
        }
    }
}
