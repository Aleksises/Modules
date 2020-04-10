namespace DAL.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EntityFramework.NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.EntityFramework.NorthwindContext context)
        {
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "Italian food" },
                new Category { CategoryName = "Best snacks" });

            context.Regions.AddOrUpdate(r => r.RegionID,
                new Region { RegionDescription = "Gomel", RegionID = 5 });

            context.Territories.AddOrUpdate(t => t.TerritoryID,
                new Territory { TerritoryID = "3330912", TerritoryDescription = "The Zavod", RegionID = 5 },
                new Territory { TerritoryID = "9120333", TerritoryDescription = "GSU", RegionID = 5 });
        }
    }
}
