using System.Collections.Generic;

namespace ConnApsDomain
{
    public interface IApartment
    {
        int Id { get; }
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
        IBuilding Building { get; }
        int TenantsAllowed { get; }
        string FacingDirection { get; }
        IEnumerable<ITenant> Tenants { get; }
    }
}