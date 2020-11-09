namespace RaceSimulator.Transport
{
    public abstract class AirTransport : ITransport
    {
        public abstract string Name { get; }
        public abstract double Speed { get; }
        protected abstract double GetDistanceReducer(int distance);
        
        public double GetRaceTime(int distance)
        {
            return distance * (1.0 - GetDistanceReducer(distance)) / Speed;
        }
    }
}