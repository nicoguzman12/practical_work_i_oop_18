using System;

namespace AirUFV
{
    public class PrivateAircraft : Aircraft //creating subclass private
    {
        public string OwnerName { get; set; }

        public PrivateAircraft(int id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, string ownerName)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            OwnerName = ownerName; 
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Private] ID: {Id}, Owner: {OwnerName}, Distance: {Distance}, Fuel: {CurrentFuel}/{FuelCapacity}, Status: {Status}"); // output of data
        }
    }
}
