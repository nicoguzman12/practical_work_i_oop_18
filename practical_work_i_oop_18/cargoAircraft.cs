using System;

namespace AirUFV
{
    public class CargoAircraft : Aircraft //creating the subclass cargo
    {
        public double MaximumLoad { get; set; }

        public CargoAircraft(int id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maximumLoad) : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel){ // with "base" we go back to the pricipal class and reuse the properties
            MaximumLoad = maximumLoad; 
    }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Cargo] ID: {Id}, MaximumLoad: {MaximumLoad}, Distance: {Distance}, Fuel: {CurrentFuel}/{FuelCapacity}, Status: {Status}"); // output of data
        }
    }
}
