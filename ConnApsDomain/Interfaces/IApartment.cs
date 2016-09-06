namespace ConnApsDomain
{
    public interface IApartment
    {
        int ApartmentId { get; }
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
        IBuilding Building { get; }
        int TenantsAllowed { get; }
        string FacingDirection { get; }
        int TenantId { get; }
        ITenant Tenant { get; }
    }
}