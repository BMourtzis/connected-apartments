using System;

namespace ConnApsDomain
{
    internal interface IPerson
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
    }
}