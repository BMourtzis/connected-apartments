namespace ConnApsDomain.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The Configurations class for the DBMigrations
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ConnApsDomain.ConnApsContext>
    {
        /// <summary>
        /// Initial configuration method
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ConnApsDomain.ConnApsContext";
        }

        /// <summary>
        /// Adds default instances, after the initial configuration
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ConnApsDomain.ConnApsContext context)
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
        }
    }
}
