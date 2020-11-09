namespace RaceSimulator.Transport.Implementations.Ground
{
    public class BactrianCamel : GroundTransport
    {
        public override string GetName() => "Bactrian camel";

        public override double GetSpeed() => 10.0;

        protected override double GetActivityTime() => 30.0;

        protected override double GetRestDuration(int restIndex) => 
            restIndex == 0 ? 5.0 : 8.0;
    }
}