using FluentAssertions;
using NUnit.Framework;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.UnitTests.DAL.RepositoryTestst
{
    [TestFixture]
    public class TableRepositoryTests : BaseRepositoryTests<Table>
    {

        [Test]
        public void Should_add_table_without_column()
        {
            var table = new Table();
            Repository.AddOrUpdate(table);
            var tableDb = Repository.Find(new PageInfoRequest { Id = table.Id });
            tableDb.Should().NotBeNull();
        }

        [Test]
        public void Should_add_table_with_column()
        {
            var table = new Table();
            table.Columns.Add(new Column());
            Repository.AddOrUpdate(table);
            var tableDb = Repository.Find(new PageInfoRequest { Id = table.Id });
            tableDb.Should().NotBeNull();
            tableDb.Columns.Should().NotBeEmpty();
        }
    }
}
