namespace RaceSimulator.Transport.Implementations.Ground
{
    public class BactrianCamel : GroundTransport
    {
        public override string Name => "Bactrian camel";

        public override double Speed => 10.0;

        protected override double ActivityTime => 30.0;

        protected override double GetRestDuration(int restIndex) => 
            restIndex == 0 ? 5.0 : 8.0;
    }
}