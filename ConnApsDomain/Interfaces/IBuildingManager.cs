using System;

namespace ConnApsDomain
{
    public interface IBuildingManager
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
        IBuilding Building { get; }
    }
}