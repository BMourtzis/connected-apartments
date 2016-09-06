namespace ConnApsDomain
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    internal class ConnApsContext : DbContext
    {
        public ConnApsContext()
            : base("name=Model")
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<BuildingManager> BuildingManagers { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
    }
}