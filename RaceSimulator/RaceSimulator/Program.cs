using System;
using RaceSimulator.Races;
using RaceSimulator.Transport;
using RaceSimulator.Transport.Implementations.Air;
using RaceSimulator.Transport.Implementations.Ground;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var race = new MixedRace(1000);
            race.RegisterVehicle(new Broom());
            race.RegisterVehicle(new Centaur());
            race.RegisterVehicle(new Stupa());
            race.RegisterVehicle(new BactrianCamel());
            race.RegisterVehicle(new FastCamel());
            race.RegisterVehicle(new MagicCarpet());
            race.RegisterVehicle(new SuperBoots());
            race.Start();
            var idx = 1;
            foreach (var result in race.Results)
            {
                Console.WriteLine($"{idx++}: {result.Vehicle.Name} - {result.Time}");
            }
            Console.WriteLine($"Winner: {race.WinnerResult.Vehicle.Name}");
            Console.WriteLine();
            
            var groundRace = new GroundRace(100000);
            groundRace.RegisterVehicle(new Centaur());
            groundRace.RegisterVehicle(new FastCamel());
            groundRace.RegisterVehicle(new BactrianCamel());
            groundRace.Start();
            Console.WriteLine($"Ground race winner: {groundRace.WinnerResult.Vehicle.Name}");
        }
    }
}