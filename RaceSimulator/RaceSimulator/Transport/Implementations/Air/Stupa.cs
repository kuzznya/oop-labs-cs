namespace RaceSimulator.Transport.Implementations.Air
{
    public class Stupa : AirTransport
    {
        public override string Name => "Stupa";

        public override double Speed => 8.0;

        protected override double GetDistanceReducer(int distance) => 0.06;
    }
}