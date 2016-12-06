using ConnApsDomain.Models;
using System.Data.Entity;

namespace ConnApsDomain
{

    internal sealed class ConnApsContext : DbContext
    {
        public ConnApsContext()
            : base("name=Model")
        {
            Buildings = Set<Building>();
            Locations = Set<Location>();
            Apartments = Set<Apartment>();
            Facilities = Set<Facility>();
            Bookings = Set<Booking>();
            People = Set<Person>();
            BuildingManagers = Set<BuildingManager>();
            Tenants = Set<Tenant>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tenant>().HasRequired(t => t.Apartment).WithMany(a => a.Tenants).WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(b => b.Facility).WithMany(f => f.Bookings).WillCascadeOnDelete(false);
            modelBuilder.Entity<Building>().HasMany(b => b.Locations);
            modelBuilder.Entity<Building>().HasMany(b => b.Managers);
            modelBuilder.Entity<Apartment>().HasMany(a => a.Tenants);
            modelBuilder.Entity<Facility>().HasMany(f => f.Bookings);
            modelBuilder.Entity<Person>().HasMany(p => p.Bookings);
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<BuildingManager> BuildingManagers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

    }
}