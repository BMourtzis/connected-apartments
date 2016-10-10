using System;
using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface IBuildingManager
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
        int BuildingId { get; }
        IBuilding Building { get; }
        IEnumerable<IBooking> Bookings { get; }
    }
}