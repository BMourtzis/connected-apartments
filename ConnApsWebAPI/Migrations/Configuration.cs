using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnApsWebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ConnApsWebAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ConnApsWebAPI.Models.ApplicationDbContext";
        }

        protected override void Seed(ConnApsWebAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Roles.AddOrUpdate(
                p => p.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "Tenant" },
                new IdentityRole { Name = "BuildingManager" }
                );
        }
    }
}
