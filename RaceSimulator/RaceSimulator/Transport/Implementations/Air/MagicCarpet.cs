namespace RaceSimulator.Transport.Implementations.Air
{
    public class MagicCarpet : AirTransport
    {
        public override string Name => "Magic carpet";

        public override double Speed => 10.0;

        protected override double GetDistanceReducer(int distance)
        {
            return distance switch
            {
                _ when distance < 1000 => 0.0,
                _ when distance < 5000 => 0.03,
                _ when distance < 10000 => 0.1,
                _ => 0.05
            };
        }
    }
}