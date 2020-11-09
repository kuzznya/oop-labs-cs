namespace RaceSimulator.Transport.Implementations.Air
{
    public class Stupa : AirTransport
    {
        public override string GetName() => "Stupa";

        public override double GetSpeed() => 8.0;

        protected override double GetDistanceReducer(int distance) => 0.06;
    }
}