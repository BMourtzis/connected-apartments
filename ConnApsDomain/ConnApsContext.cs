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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tenant>().HasRequired(t => t.Aparment).WithMany(a => a.Tenants).WillCascadeOnDelete(false);
            modelBuilder.Entity<Building>().HasMany(b => b.Locations);
            modelBuilder.Entity<Building>().HasMany(b => b.Managers);
            modelBuilder.Entity<Apartment>().HasMany(a => a.Tenants);
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<BuildingManager> BuildingManagers { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }

    }
}