using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Remont.Common;
using Remont.Common.Model;
using Remont.DAL;
using Remont.WebUI.Controllers.Api;

namespace Remont.UnitTests.WebApi
{
	[TestFixture]
	public class TableTests
	{
		private TableController _controller;

		[SetUp]
		public void SetupTest()
		{
			_controller = new TableController(new EntityRepository<Table, int>(), new EntityRepository<Column, int>());
		}

		[Test]
		public void Should_not_throw_ex_on_get()
		{
			_controller.Invoking(c=>c.Get(new PageInfoRequest<int>())).ShouldNotThrow();
		}
	}
}
