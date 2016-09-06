using System.Collections;
using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface IBuilding
    {
        string BuildingName { get; }
        string Address { get; }
        IEnumerable<ILocation> Locations { get; }
        IBuildingManager BuildingManager { get; }

    }
}