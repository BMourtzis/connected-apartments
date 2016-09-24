namespace ConnApsDomain
{
    public interface ILocation
    {
        int Id { get; }
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
        IBuilding Building { get; }
    }
}