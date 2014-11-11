using System.Data.Entity;
using Remont.Common.Model;

namespace Remont.DAL
{
    public class RemontContext : DbContext
    {
        public RemontContext()
            : base("Remont")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");

            base.OnModelCreating(modelBuilder);
        }
    }
}
