using System;

namespace AirUFV
{
    public abstract class Aircraft //creating abstract class
    {
        public int Id { get; set; } //{get;set} to encapsulate the properties and do the code cleaner
        public AircraftStatus Status { get; set; }
        public int Distance { get; set; }
        public int Speed { get; set; }
        public double FuelCapacity { get; set; }
        public double FuelConsumption { get; set; }
        public double CurrentFuel { get; set; }

        public Aircraft(int id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel) // construction of the class
        {
            Id = id;
            Status = status;
            Distance = distance;
            Speed = speed;
            FuelCapacity = fuelCapacity;
            FuelConsumption = fuelConsumption;
            CurrentFuel = currentFuel;
        }

        public abstract void ShowInfo();

        public void UpdateDistanceAndFuel(double timeHours) //to know data as fuelconsumption, distance
        {
            int traveled = (int)(Speed * timeHours);
            Distance = Math.Max(0, Distance - traveled);
            CurrentFuel = Math.Max(0, CurrentFuel - traveled * FuelConsumption);

            // the aircraft that is not moving doesn't change state
            if (Distance == 0 && Status == AircraftStatus.InFlight)
                Status = AircraftStatus.Waiting;
        }
    }
}
