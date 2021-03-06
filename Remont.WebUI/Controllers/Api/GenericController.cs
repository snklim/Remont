﻿using System.Linq;
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
