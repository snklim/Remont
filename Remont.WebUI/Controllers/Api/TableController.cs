﻿using System;
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
    public class TableController : RemontController<Table, int>
    {
        private readonly IRepository<Column, int> _columnRepository;

        public TableController(IRepository<Table, int> tableTepository, IRepository<Column, int> columnRepository)
            : base(tableTepository)
        {
            _columnRepository = columnRepository;
        }

        public override int Post(Table item)
        {
            int tableId = base.Post(item);
            
            if (item.Columns != null)
            {
                item.Columns.ForEach(c =>
                {
                    c.TableId = tableId;
                    _columnRepository.AddOrUpdate(c);
                });
            }

            return tableId;
        }
    }
}
