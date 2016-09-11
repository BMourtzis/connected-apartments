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
        int BuildingId { get; }
        IBuilding Building { get; }
    }
}