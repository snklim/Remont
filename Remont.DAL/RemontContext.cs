using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Remont.Common.Model;

namespace Remont.DAL
{
    public class RemontContext : DbContext
    {
        public RemontContext()
            : base("Remont")
        {
            Database.SetInitializer(new RemontContextInitializer());
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        public virtual DbSet<Table> Tables { get; set; }

		public virtual DbSet<Column> Columns { get; set; }

		public virtual DbSet<Control> Controls { get; set; }

		public virtual DbSet<Row> Rows { get; set; }

		public virtual DbSet<Cell> Cells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Entity<Row>().ToTable("Row");
			modelBuilder.Entity<Cell>().ToTable("Cell");
			modelBuilder.Entity<Customer>().ToTable("Customer");
			modelBuilder.Entity<Control>().ToTable("Control");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Table>().ToTable("Table");
            modelBuilder.Entity<Column>().ToTable("Column");
			
            base.OnModelCreating(modelBuilder);
        }
    }
}
