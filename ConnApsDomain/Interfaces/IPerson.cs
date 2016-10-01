using System;
using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface IPerson
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
        IEnumerable<IBooking> Bookings { get; }
    }
}