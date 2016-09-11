using System.Collections;
using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface IBuilding
    {
        int Id { get; }
        string BuildingName { get; }
        string Address { get; }
        IEnumerable<ILocation> Locations { get; }
        IEnumerable<IBuildingManager> BuildingManagers { get; }
    }
}