namespace RaceSimulator.Transport.Implementations.Ground
{
    public class Centaur : GroundTransport
    {
        public override string GetName() => "Centaur";

        public override double GetSpeed() => 15.0;

        protected override double GetActivityTime() => 8.0;

        protected override double GetRestDuration(int restIndex) => 2.0;
    }
}