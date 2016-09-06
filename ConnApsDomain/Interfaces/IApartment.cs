namespace ConnApsDomain
{
    public interface IApartment
    {
        string Level { get; }
        string Number { get; }
        IBuilding Building { get; }
        int TenantsAllowed { get; }
        string FacingDirection { get; }
        ITenant Tenant { get; }
    }
}