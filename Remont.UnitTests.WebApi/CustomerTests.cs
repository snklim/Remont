using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Remont.Common;
using Remont.Common.Model;
using Remont.DAL;
using Remont.WebUI.Controllers.Api;

namespace Remont.UnitTests.WebApi
{
    [TestFixture]
    public class CustomerTests
    {
        private CustomerController _controller;
        private Customer _customer;

        [SetUp]
        public void SetupTest()
        {
            // TODO: add IoC
            _controller = new CustomerController(new EntityRepository<Customer, int>());

            _customer = new Customer
            {
                Email = "email@remont.net",
                FirstName = "Ivan",
                LastName = "Sotnik",
                PhoneNumber = "555-55-55",
                Orders = new Order[0]
            };
        }

        [TearDown]
        public void ClearTest()
        {
            using (var db = new RemontContext())
            {
                if (_customer != null)
                {
                    _customer = db.Customers.Find(_customer.Id);
                    db.Customers.Remove(_customer);
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void Should_create()
        {
            _customer = _controller.Post(_customer);
            using (var db = new RemontContext())
            {
                var customerDb = db.Customers.Find(_customer.Id);
                customerDb.ShouldBeEquivalentTo(_customer);
            }
        }

        [Test]
        public void Should_update()
        {
            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
            }

            _customer.Email = "email1@remont.net";
            _customer.FirstName += " CHANGES";
            _customer.LastName += " CHANGES";
            _customer.PhoneNumber += " CHANGES";

            _controller.Post(_customer);
            
            using (var db = new RemontContext())
            {
                var customerDb = db.Customers.Find(_customer.Id);
                customerDb.ShouldBeEquivalentTo(_customer);
            }
        }

        [Test]
        public void Should_delete()
        {
            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
                db.SaveChanges();
            }

            _controller.Delete(_customer.Id);

            using (var db = new RemontContext())
            {
                _customer = db.Customers.Find(_customer.Id);
            }

            _customer.Should().BeNull();
        }

        [Test]
        public void Should_get()
        {
            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
                db.SaveChanges();
            }

            var result = _controller.Get(new PageInfoRequest<int>());

            var customerApi = result.Items.FirstOrDefault(c => c.Id == _customer.Id);

            customerApi.Should().NotBeNull();
        }

        [Test]
        public void Should_get_by_id()
        {
            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
                db.SaveChanges();
            }

            var result = _controller.Get(new PageInfoRequest<int> {Id = _customer.Id});
            result.Item.ShouldBeEquivalentTo(_customer);
        }
    }
}