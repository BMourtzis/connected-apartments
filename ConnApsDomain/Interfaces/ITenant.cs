using System;
using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface ITenant
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        int ApartmentId { get; }
        string UserId { get; }
        int BuildingId { get; }
        IApartment Apartment { get; }
        IEnumerable<IBooking> Bookings { get; }

    }
}