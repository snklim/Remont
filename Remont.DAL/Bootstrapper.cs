using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Remont.Common.Model;
using Remont.Common.Repository;
using Remont.DAL.Repositories;

namespace Remont.DAL
{
    public static class Bootstrapper
    {
        public static void Setup(IUnityContainer container)
        {
            container.RegisterType<IRepository<Table>, TableRepository>();
            container.RegisterType<IRepository<Control>, EntityRepository<Control>>();

            container.RegisterType<IRepository<Column>, ColumnRepository>();
            container.RegisterType<IRepository<Row>, RowRepository>();
            container.RegisterType<IRepository<Cell>, CellRepository>();
        }
    }
}
