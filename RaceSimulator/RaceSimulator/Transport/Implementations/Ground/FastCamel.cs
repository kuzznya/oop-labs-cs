namespace RaceSimulator.Transport.Implementations.Ground
{
    public class FastCamel : GroundTransport
    {
        public override string GetName() => "Fast camel";

        public override double GetSpeed() => 40.0;

        protected override double GetActivityTime() => 10.0;

        protected override double GetRestDuration(int restIndex)
        {
            return restIndex switch
            {
                0 => 5.0,
                1 => 6.5,
                _ => 8.0
            };
        }
    }
}