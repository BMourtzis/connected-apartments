using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ConnApsDomain.Models
{
    internal class Building: IBuilding
    {
        internal ICollection<Location> Locations { get; set; }
        internal ICollection<BuildingManager> Managers { get; set; }

        #region Constructors

        protected Building() { }

        public Building(string buildingname, string newAddress)
        {
            BuildingName = buildingname;
            Address = newAddress;
        }

        #endregion

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string BuildingName { get; private set; }

        [Required]
        public string Address { get; private set; }

        public IEnumerable<Apartment> Apartments => Locations?.OfType<Apartment>();

        public IEnumerable<Facility> Facilities => Locations?.OfType<Facility>();

        #endregion

        #region Functions

        public void UpdateBuilding(string buildingName, string address)
        {
            BuildingName = buildingName;
            Address = address;
        }

        public Apartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection)
        {
            var ap = new Apartment(level, number, tenantsAllowed, facingDirection, Id);
            return ap;
        }

        public Apartment FetchApartment(int aptId)
        {
            var apartment = Apartments.FirstOrDefault(a => a.Id == aptId);
            return apartment;
        }

        public Facility CreateFacility(string level, string number)
        {
            Facility f = new Facility(level, number, Id);
            return f;
        }

        public Facility FetchFacility(int facilityId)
        {
            var facility = Facilities.FirstOrDefault(f => f.Id == facilityId);
            return facility;
        }

        public Facility UpdateFacility(int facilityId, string level, string number)
        {
            var facility = FetchFacility(facilityId);
            facility.UpdateFacility(level, number);
            return facility;
        }

        public Booking CreateBooking(int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            var facility = Facilities.FirstOrDefault(f => f.Id == facilityId);
            if (facility != null)
            {
                var booking = facility.CreateBooking(personId, startTime, endTime);
                return booking;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion


    }

    public interface IBuilding
    {
        int Id { get; }
        string BuildingName { get; }
        string Address { get; }
    }
}
