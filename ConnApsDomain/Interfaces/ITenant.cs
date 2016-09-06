using System;

namespace ConnApsDomain
{
    public interface ITenant
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
        IApartment Apartment { get; }

    }
}