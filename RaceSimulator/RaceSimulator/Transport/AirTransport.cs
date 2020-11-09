namespace RaceSimulator.Transport
{
    public abstract class AirTransport : ITransport
    {
        public abstract string GetName();
        public abstract double GetSpeed();
        protected abstract double GetDistanceReducer(int distance);
        
        public double GetRaceTime(int distance)
        {
            return GetSpeed() * distance * (1.0 - GetDistanceReducer(distance));
        }
    }
}