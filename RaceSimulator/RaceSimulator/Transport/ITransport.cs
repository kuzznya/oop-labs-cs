namespace RaceSimulator.Transport
{
    public interface ITransport
    {
        string Name { get; }
        double Speed { get; }
        public double GetRaceTime(int distance);
    }
}