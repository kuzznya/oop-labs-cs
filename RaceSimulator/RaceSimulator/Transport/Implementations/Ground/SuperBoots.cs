namespace RaceSimulator.Transport.Implementations.Ground
{
    public class SuperBoots : GroundTransport
    {
        public override string GetName() => "Super boots";

        public override double GetSpeed() => 6.0;

        protected override double GetActivityTime() => 60.0;

        protected override double GetRestDuration(int restIndex) => 
            restIndex == 0 ? 10.0 : 5.0;
    }
}