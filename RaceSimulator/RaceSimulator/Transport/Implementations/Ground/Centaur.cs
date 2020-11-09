namespace RaceSimulator.Transport.Implementations.Ground
{
    public class Centaur : GroundTransport
    {
        public override string Name => "Centaur";

        public override double Speed => 15.0;

        protected override double ActivityTime => 8.0;

        protected override double GetRestDuration(int restIndex) => 2.0;
    }
}