using System;

namespace AirUFV
{
    public class CommercialAircraft : Aircraft //creating the subclass commercial
    {
        public int NumPassengers { get; set; }

        public CommercialAircraft(int id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maximumLoad) : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel){ // with "base" we go back to the pricipal class and reuse the properties
            NumPassengers = NumPassengers; 
    }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Commercial] ID: {Id}, The number of passengers is: {NumPassengers}, Distance: {Distance}, Fuel: {CurrentFuel}/{FuelCapacity}, Status: {Status}"); // output of data
        }
    }
}