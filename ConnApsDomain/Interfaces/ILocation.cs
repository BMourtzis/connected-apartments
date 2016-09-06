namespace ConnApsDomain
{
    public interface ILocation
    {
        string Level { get; }
        string Number { get; }
        IBuilding Building { get; }
    }
}