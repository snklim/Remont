﻿using System.Linq;
using FluentAssertions;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Remont.Common;
using Remont.Common.Model;
using Remont.Common.Repository;

namespace Remont.UnitTests.DAL.RepositoryTestst
{
    [TestFixture]
    public class RowRepositoryTests : BaseRepositoryTests<Row>
    {
        private Row _row;

        [SetUp]
        public void SetupCell()
        {
            var table = new Table();
            var column = new Column { Table = table };

            _row = new Row();
            _row.Cells.Add(new Cell { Column = column });
        }

        [Test]
        public void Should_add_data_source_rows_for_new_cell()
        {
            var table = new Table();
            var column = new Column { Table = table };
            var row = new Row();
            row.Cells.Add(new Cell {Column = column, Row = row, Value = "SOME VALUE"});

            Repository.AddOrUpdate(row);

            _row.Cells.First().DataSourceRows.Add(row);
            Repository.AddOrUpdate(_row);

            var rowDb = Repository.Find(new PageInfoRequest { Id = _row.Id, TableId = _row.TableId });

            rowDb.Cells.First().DataSourceRows.Should().NotBeEmpty();
            rowDb.Cells.First().DataSourceRows.First().Id.ShouldBeEquivalentTo(row.Id);
        }

        [Test]
        public void Should_store_cell_data_source_rows_for_exist_cell()
        {
            Repository.AddOrUpdate(_row);
            _row = Repository.Find(new PageInfoRequest { Id = _row.Id, TableId = _row.TableId });

            var table = new Table();
            var column = new Column { Table = table };
            var row = new Row();
            row.Cells.Add(new Cell {Column = column, Row = row, Value = "SOME VALUE"});

            Repository.AddOrUpdate(row);

            _row.Cells.First().DataSourceRows.Add(row);

            Repository.AddOrUpdate(_row);
            var rowDb = Repository.Find(new PageInfoRequest { Id = _row.Id, TableId = _row.TableId });

            rowDb.Cells.First().DataSourceRows.Should().NotBeEmpty();
            rowDb.Cells.First().DataSourceRows.First().Id.ShouldBeEquivalentTo(row.Id);
        }

        [Test]
        public void Should_add_new_cell_data_source_rows_for_exist_cell()
        {
            var table = new Table();
            var column = new Column {Table = table};
            var row1 = new Row {Table = table};
            var row2 = new Row {Table = table};
            row1.Cells.Add(new Cell { Column = column, Table = table });
            row2.Cells.Add(new Cell { Column = column, Table = table });

            Repository.AddOrUpdate(row1);
            Repository.AddOrUpdate(row2);

            var tableHolder = new Table();
            var columnHolder = new Column();
            var rowHolder = new Row {Table = tableHolder};
            rowHolder.Cells.Add(new Cell {Column = columnHolder, Table = tableHolder});

            rowHolder.Cells.First().DataSourceRows.Add(row1);

            Repository.AddOrUpdate(rowHolder);
            rowHolder.Cells.First().DataSourceRows.Add(row2);
            Repository.AddOrUpdate(rowHolder);

            rowHolder = Repository.Find(new PageInfoRequest {Id = rowHolder.Id});

            rowHolder.Cells.First().DataSourceRows.Count().Should().Be(2);
        }

        [Test]
        public void Should_store_two_rows_into_cell_data_source_rows()
        {
            var table = new Table();
            table.Columns.Add(new Column {Table = table});
            using (var repo = Container.Resolve<IRepository<Table>>())
            {
                repo.AddOrUpdate(table);
                table = repo.Find(new PageInfoRequest {Id = table.Id});
            }
            var row1 = new Row {Table = table};
            var row2 = new Row {Table = table};

            row1.Cells.Add(new Cell {Row = row1, Table = table, Column = table.Columns.First()});
            row2.Cells.Add(new Cell {Row = row2, Table = table, Column = table.Columns.First()});

            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                repo.AddOrUpdate(row1);
            }
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                repo.AddOrUpdate(row2);
            }

            var tableHolder = new Table();
            tableHolder.Columns.Add(new Column());
            using (var repo = Container.Resolve<IRepository<Table>>())
            {
                repo.AddOrUpdate(tableHolder);
                tableHolder = repo.Find(new PageInfoRequest { Id = table.Id });
            }

            using (var repo = Container.Resolve<IRepository<Table>>())
            {
                repo.AddOrUpdate(tableHolder);
                tableHolder = repo.Find(new PageInfoRequest { Id = tableHolder.Id });
            }

            var rowHolder = new Row {Table = tableHolder};
            rowHolder.Cells.Add(new Cell
            {
                Row = rowHolder,
                Table = tableHolder,
                Column = tableHolder.Columns.First()
            });
            
            rowHolder.Cells.First().DataSourceRows.Add(row1);
            rowHolder.Cells.First().DataSourceRows.Add(row2);
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                repo.AddOrUpdate(rowHolder);
            }
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                rowHolder = repo.Find(new PageInfoRequest {Id = rowHolder.Id});
            }
            rowHolder.Cells.First().DataSourceRows.Count().Should().Be(2);
        }

        [Test]
        public void Should_store_data_source_rows_several_requests()
        {
            int row1;
            int table1;
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                var table = new Table();
                var column = new Column {Table = table};
                var row = new Row {Table = table};
                row.Cells.Add(new Cell {Column = column, Table = table});
                repo.AddOrUpdate(row);
                row1 = row.Id;
                table1 = row.TableId;
            }

            int row2;
            int table2;
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                var table = new Table();
                var column = new Column {Table = table};
                var row = new Row {Table = table};
                row.Cells.Add(new Cell {Column = column, Table = table});
                repo.AddOrUpdate(row);
                row2 = row.Id;
                table2 = row.TableId;
            }

            Row rowDb1;
            Row rowDb2;
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                rowDb1 = repo.Find(new PageInfoRequest {Id = row1, TableId = table1});
                rowDb2 = repo.Find(new PageInfoRequest {Id = row2, TableId = table2});
            }

            rowDb1.Cells.First().DataSourceRows.Add(rowDb2);
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                repo.AddOrUpdate(rowDb1);
            }

            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                rowDb1 = repo.Find(new PageInfoRequest {Id = rowDb1.Id});
            }

            rowDb1.Cells.First().DataSourceRows.Should().NotBeEmpty();

            int row3;
            int table3;
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                var table = new Table();
                var column = new Column { Table = table };
                var row = new Row { Table = table };
                row.Cells.Add(new Cell { Column = column, Table = table });
                repo.AddOrUpdate(row);
                row3 = row.Id;
                table3 = row.TableId;
            }

            Row rowDb3;
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                rowDb3 = repo.Find(new PageInfoRequest { Id = row3, TableId = table3 });
            }

            rowDb1.Cells.First().DataSourceRows.Add(rowDb3);
            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                repo.AddOrUpdate(rowDb1);
            }

            using (var repo = Container.Resolve<IRepository<Row>>())
            {
                rowDb1 = repo.Find(new PageInfoRequest { Id = rowDb1.Id });
            }

            rowDb1.Cells.First().DataSourceRows.Count().Should().Be(2);
        }
    }
}
