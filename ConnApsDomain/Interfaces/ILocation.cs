namespace ConnApsDomain
{
    public interface ILocation
    {
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
        IBuilding Building { get; }
    }
}