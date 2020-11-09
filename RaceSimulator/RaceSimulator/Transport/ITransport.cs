namespace RaceSimulator.Transport
{
    public interface ITransport
    {
        public string GetName();
        public double GetSpeed();
        public double GetRaceTime(int distance);
    }
}