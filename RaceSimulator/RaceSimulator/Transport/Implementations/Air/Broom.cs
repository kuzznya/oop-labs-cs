namespace RaceSimulator.Transport.Implementations.Air
{
    public class Broom : AirTransport
    {
        public override string Name => "Broom";

        public override double Speed => 20.0;

        protected override double GetDistanceReducer(int distance) => distance / 1000 * 0.01;
    }
}