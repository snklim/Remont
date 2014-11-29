using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Remont.Common.Model;
using Remont.Common.Repository;
using Remont.DAL;

namespace Remont.UnitTests.DAL.RepositoryTestst
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

            using (var repo = Container.Resolve<IRepository<Cell>>())
            {
                repo.GetAll().ToList().ForEach(item => repo.Delete(item));
            }

            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                repo.GetAll().ToList().ForEach(item => repo.Delete(item));
            }

            using (var repo = Container.Resolve<IRepository<Column>>())
            {
                repo.GetAll().ToList().ForEach(item => repo.Delete(item));
            }

            using (var repo = Container.Resolve<IRepository<Table>>())
            {
                repo.GetAll().ToList().ForEach(item => repo.Delete(item));
            }
        }
    }
}
