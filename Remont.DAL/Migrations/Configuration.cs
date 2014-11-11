namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Remont.DAL.RemontContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Remont.DAL.RemontContext";
        }

        protected override void Seed(Remont.DAL.RemontContext context)
        {
            context.OrderStatuses.Add(new Common.Model.OrderStatus
            {
                StatusName = "New"
            });

            context.OrderStatuses.Add(new Common.Model.OrderStatus
            {
                StatusName = "In Progress"
            });

            context.OrderStatuses.Add(new Common.Model.OrderStatus
            {
                StatusName = "Completed"
            });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
