namespace RaceSimulator.Transport.Implementations.Air
{
    public class Broom : AirTransport
    {
        public override string GetName() => "Broom";

        public override double GetSpeed() => 20.0;

        protected override double GetDistanceReducer(int distance) => distance / 1000 * 0.01;
    }
}