namespace RaceSimulator.Transport
{
    public abstract class GroundTransport : ITransport
    {
        public abstract string Name { get; }
        public abstract double Speed { get; }
        protected abstract double ActivityTime { get; }
        protected abstract double GetRestDuration(int restIndex);

        public double GetRaceTime(int distance)
        {
            var restCount = (int) (distance / Speed / ActivityTime);
            double totalRestTime = 0;
            for (var i = 0; i < restCount; i++)
                totalRestTime += GetRestDuration(i);
            return distance / Speed + totalRestTime;
        }
    }
}