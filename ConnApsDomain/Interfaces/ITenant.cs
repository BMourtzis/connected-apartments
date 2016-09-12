using System;

namespace ConnApsDomain
{
    public interface ITenant
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        int ApartmentId { get; }
        string UserId { get; }
        int BuildingId { get; }
        IApartment Apartment { get; }

    }
}