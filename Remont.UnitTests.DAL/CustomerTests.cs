using System.Data.Entity.Migrations;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Remont.Common.Model;
using Remont.DAL;

namespace Remont.UnitTests.DAL
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer _customer;

        [SetUp]
        public void SetupTest()
        {
            _customer = new Customer
            {
                Email = "email@remont.net",
                FirstName = "Ivan",
                LastName = "Sotnik",
                PhoneNumber = "555-55-55"
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
            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
                db.SaveChanges();
            }

            _customer.Id.Should().BePositive();
        }

        [Test]
        public void Should_update()
        {
            const string newEmail = "email1@remont.net";

            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
                db.SaveChanges();
            }

            using (var db = new RemontContext())
            {
                _customer.Email = "email1@remont.net";
                db.Customers.AddOrUpdate(_customer);
                db.SaveChanges();
            }

            using (var db = new RemontContext())
            {
                _customer = db.Customers.FirstOrDefault(c => c.Id == _customer.Id);
            }

            if (_customer != null) _customer.Email.ShouldBeEquivalentTo(newEmail);
        }

        [Test]
        public void Should_delete()
        {
            using (var db = new RemontContext())
            {
                db.Customers.Add(_customer);
                db.SaveChanges();
            }

            using (var db = new RemontContext())
            {
                db.Customers.RemoveRange(db.Customers.Where(c => c.Id == _customer.Id));
                db.SaveChanges();
            }

            using (var db = new RemontContext())
            {
                _customer = db.Customers.FirstOrDefault(c => c.Id == _customer.Id);
            }

            _customer.Should().BeNull();
        }
    }
}