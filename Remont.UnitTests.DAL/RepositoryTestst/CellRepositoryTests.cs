using FluentAssertions;
using NUnit.Framework;
using Remont.Common;
using Remont.Common.Model;

namespace Remont.UnitTests.DAL.RepositoryTestst
{
    [TestFixture]
    public class CellRepositoryTests : BaseRepositoryTests<Cell>
    {
        private Cell _cell;

        [SetUp]
        public void SetupCell()
        {
            var table = new Table();
            var column = new Column { Table = table };
            var row = new Row();
            
            _cell = new Cell { Column = column, Row = row };
        }

        [Test]
        public void Should_add_cell_with_no_ex()
        {
            Repository.Invoking(r => r.AddOrUpdate(_cell)).ShouldNotThrow();
        }

        [Test]
        public void Should_read_cell_val()
        {
            _cell.Value = "SOME VALUE";
            Repository.AddOrUpdate(_cell);

            var cellDb = Repository.Find(new PageInfoRequest {Id = _cell.Id, TableId = _cell.TableId});
            _cell.Value.ShouldBeEquivalentTo(cellDb.Value);
        }

        [Test]
        public void Should_store_cell_data_source_row()
        {
            var table = new Table();
            var column = new Column { Table = table };
            var row = new Row();
            var cell = new Cell { Column = column, Row = row, Value = "SOME VALUE"};

            Repository.AddOrUpdate(cell);

            _cell.DataSourceRowId = row.Id;
            Repository.AddOrUpdate(_cell);

            var cellDb = Repository.Find(new PageInfoRequest {Id = _cell.Id, TableId = _cell.TableId});

            cellDb.DataSourceRow.Should().NotBeNull();
        }

        [Test]
        public void Should_store_cell_data_source_rows_for_new_cell()
        {
            var table = new Table();
            var column = new Column { Table = table };
            var row = new Row();
            var cell = new Cell { Column = column, Row = row, Value = "SOME VALUE" };

            Repository.AddOrUpdate(cell);

            _cell.DataSourceRows.Add(row);
            Repository.AddOrUpdate(_cell);

            var cellDb = Repository.Find(new PageInfoRequest { Id = _cell.Id, TableId = _cell.TableId });

            cellDb.DataSourceRows.Should().NotBeEmpty();
        }

        [Test]
        public void Should_store_cell_data_source_rows_for_exist_cell()
        {
            Repository.AddOrUpdate(_cell);
            _cell = Repository.Find(new PageInfoRequest { Id = _cell.Id, TableId = _cell.TableId });

            var table = new Table();
            var column = new Column { Table = table };
            var row = new Row();
            var cell = new Cell { Column = column, Row = row, Value = "SOME VALUE" };

            Repository.AddOrUpdate(cell);

            _cell.DataSourceRows.Add(row);

            Repository.AddOrUpdate(_cell);
            _cell = Repository.Find(new PageInfoRequest {Id = _cell.Id, TableId = _cell.TableId});

            _cell.DataSourceRows.Should().NotBeEmpty();
        }
    }
}
