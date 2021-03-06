﻿using System.Linq;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;
using Remont.DAL;
using Remont.DAL.Repositories;

namespace Remont.WebUI.Controllers.Api
{
	public class TableController : RemontController<Table, PageInfoRequest>
    {
        private readonly EntityRepository<Column> _columnRepository;

        public TableController(IRepository<Table> tableRepository, EntityRepository<Column> columnRepository)
            : base(tableRepository)
        {
            _columnRepository = columnRepository;
        }
    }
}
