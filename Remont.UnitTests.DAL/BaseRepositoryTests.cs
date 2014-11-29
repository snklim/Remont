using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Remont.Common.Model;
using Remont.Common.Repository;
using Remont.DAL;
using Remont.DAL.Repositories;

namespace Remont.UnitTests.DAL
{
    public class BaseRepositoryTests<TItem>
        where TItem : BaseItem
    {
        protected IRepository<TItem> Repository;
        protected UnityContainer Container;

        [SetUp]
        public void CreateRepository()
        {
            Container = new UnityContainer();
            Bootstrapper.Setup(Container);
            Repository = Container.Resolve<IRepository<TItem>>();
        }

        [TearDown]
        public void DisposeRepository()
        {
            Repository.Dispose();
        }
    }
}
