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
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new RemontContextInitializer());
        }
        
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
            modelBuilder.Entity<Cell>().HasOptional(cell => cell.DataSourceRow)
                .WithMany().HasForeignKey(cell => cell.DataSourceRowId);
	        modelBuilder.Entity<Cell>().HasMany(cell => cell.DataSourceRows)
		        .WithMany();

			modelBuilder.Entity<Control>().ToTable("Control");
            modelBuilder.Entity<Table>().ToTable("Table");
            modelBuilder.Entity<Column>().ToTable("Column");
			
            base.OnModelCreating(modelBuilder);
        }
    }
}
