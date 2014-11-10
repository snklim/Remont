using System.Data.Entity.Migrations;
using System.Web.Http;

namespace Remont.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        public int Put(DAL.CUSTOMER customer)
        {
            using (var db = new Remont.DAL.Remont())
            {
                db.CUSTOMERs.Add(customer);
                db.SaveChanges();
            }
            return customer.CUSTOMER_ID;
        }

        public int Post(DAL.CUSTOMER customer)
        {
            using (var db = new Remont.DAL.Remont())
            {
                db.CUSTOMERs.AddOrUpdate(customer);
                db.SaveChanges();
            }
            return customer.CUSTOMER_ID;
        }

        public void Delete(int customerId)
        {
            using (var db = new Remont.DAL.Remont())
            {
                var customerDb = db.CUSTOMERs.Find(customerId);
                if (customerDb != null)
                {
                    db.CUSTOMERs.Remove(customerDb);
                    db.SaveChanges();
                }
            }
        }
    }
}
