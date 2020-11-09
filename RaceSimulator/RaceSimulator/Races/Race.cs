using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RaceSimulator.Transport;

namespace RaceSimulator.Races
{
    public enum RaceStatus
    {
        Open,
        Finished
    }

    public class RaceResult<T> where T : ITransport
    {
        public T Vehicle { get; }
        public double Time { get; }

        public RaceResult(T vehicle, double time)
        {
            Vehicle = vehicle;
            Time = time;
        }
    }
    
    public abstract class Race<T> where T : ITransport
    {
        public int Distance { get; }
        
        private List<T> _vehicles = new List<T>();

        public RaceStatus Status { get; private set; } = RaceStatus.Open;


        public ImmutableList<RaceResult<T>> Results;

        protected Race(int distance)
        {
            Distance = distance;
        }

        public void RegisterVehicle(T vehicle)
        {
            if (Status == RaceStatus.Finished)
                throw new InvalidOperationException("Race is finished");
            _vehicles.Add(vehicle);
        }

        public void Start()
        {
            if (Status == RaceStatus.Finished)
                throw new InvalidOperationException("Race is finished");
            
            if (_vehicles.Count == 0)
                throw new InvalidOperationException("No registered vehicles");
            
            var results = new List<RaceResult<T>>();
            
            foreach (var vehicle in _vehicles)
            {
                var time = vehicle.GetRaceTime(Distance);
                results.Add(new RaceResult<T>(vehicle, time));
            }
            
            results.Sort((result1, result2) => result1.Time.CompareTo(result2.Time));
            Results = results.ToImmutableList();

            Status = RaceStatus.Finished;
        }

        public RaceResult<T> WinnerResult
        {
            get
            {
                if (Status == RaceStatus.Open)
                    throw new InvalidOperationException("Race is not finished");

                return Results[0];
            }
        }
    }

    public class AirRace : Race<AirTransport>
    {
        public AirRace(int distance) : base(distance) { }
    }

    public class GroundRace : Race<GroundTransport>
    {
        public GroundRace(int distance) : base(distance) { }
    }

    public class MixedRace : Race<ITransport>
    {
        public MixedRace(int distance) : base(distance) { }
    }
}