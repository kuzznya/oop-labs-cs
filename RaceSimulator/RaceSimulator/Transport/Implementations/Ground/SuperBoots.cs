namespace RaceSimulator.Transport.Implementations.Ground
{
    public class SuperBoots : GroundTransport
    {
        public override string Name => "Super boots";

        public override double Speed => 6.0;

        protected override double ActivityTime => 60.0;

        protected override double GetRestDuration(int restIndex) => 
            restIndex == 0 ? 10.0 : 5.0;
    }
}