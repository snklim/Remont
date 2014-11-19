using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.WebUI.Controllers.Api
{
    public class TableData
    {
        public int TableId { get; set; }

        public int RecordId { get; set; }

        public string[] Values { get; set; }
    }

    public class GenericController : ApiController
    {
        private readonly IRepository<Table, int> _tableRepository;
        private readonly IRepository<Column, int> _columnRepository;
        private readonly IRepository<Row, int> _rowRepository;

        public GenericController(IRepository<Table, int> tableRepository, 
            IRepository<Column, int> columnRepository,
            IRepository<Row, int> rowRepository)
        {
            _tableRepository = tableRepository;
            _columnRepository = columnRepository;
            _rowRepository = rowRepository;
        }

        [Route("api/generic/{tableId:int}/{recordId:int?}")]
        public Response<Table, int> Get(int tableId, int recordId = -1)
        {
            var table = _tableRepository.Find(tableId);

            if (table == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            table.Columns = _columnRepository.GetAll(items => items.Where(item => item.TableId == tableId));

            object rows = null;

            if (recordId == -1)
            {
                rows = _rowRepository
                    .GetAll(items => items.Where(item => item.TableId == tableId))
                    .GroupBy(r => r.RecordId);
            }

            return new Response<Table, int>
            {
                Items = new[] {table},
                Bag = rows,
                PageInfoRequest = new PageInfoRequest<int>
                {
                    Id = 1,
                    PageIndex = 0,
                    TotalItems = 1,
                    TotalPages = 1
                }
            };
        }

        public int Post(TableData data)
        {
            var table = _tableRepository.Find(data.TableId);

            if (table == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var recordId = data.RecordId;

            if (recordId <= 0)
            {
                var rows = _rowRepository
                    .GetAll(items => items.Where(r => r.TableId == data.TableId));
                if (rows.Any())
                    recordId = rows.Max(r => r.RecordId) + 1;

                var columns = _columnRepository.GetAll(items => items.Where(item => item.TableId == data.TableId));

                var index = 0;
                columns.ForEach(c =>
                {
                    var row = new Row
                    {
                        ColumnId = c.Id,
                        //Column = c,
                        //Table = table,
                        RecordId = recordId,
                        TableId = data.TableId,
                        Value = index < data.Values.Length ? data.Values[index] : string.Empty
                    };

                    _rowRepository.AddOrUpdate(row);

                    index++;
                });

            }

            return recordId;
        }
    }
}
