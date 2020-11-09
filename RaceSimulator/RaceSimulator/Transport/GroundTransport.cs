namespace RaceSimulator.Transport
{
    public abstract class GroundTransport : ITransport
    {
        public abstract string GetName();
        public abstract double GetSpeed();
        protected abstract double GetActivityTime();
        protected abstract double GetRestDuration(int restIndex);

        public double GetRaceTime(int distance)
        {
            var restCount = (int) (distance / GetSpeed() / GetActivityTime());
            double totalRestTime = 0;
            for (var i = 0; i < restCount; i++)
                totalRestTime += GetRestDuration(i);
            return distance / GetSpeed() + totalRestTime;
        }
    }
}