using System.Collections;
using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface IBuilding
    {
        int BuildingId { get; }
        string BuildingName { get; }
        string Address { get; }
        IEnumerable<ILocation> Locations { get; }
        IBuildingManager BuildingManager { get; }

    }
}