namespace EFTransaction.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EFTransaction.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EFTransaction.Models.MyEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EFTransaction.Models.MyEntities context)
        {
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

            context.Products.AddOrUpdate(
              p => p.ProductName,
              new Product { ProductName = "Andrew Peters", Price = 50.55M },
              new Product { ProductName = "Brice Lambson", Price = 40.8M },
              new Product { ProductName = "Rowan Miller", Price = 12.00M }
            );
        }
    }
}
