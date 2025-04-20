using System;

namespace AirUFV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var plane1 = new CommercialAircraft(1, AircraftStatus.InFlight, 300, 900, 20000, 3.5, 15000, 180); // comercial
            var plane2 = new CargoAircraft(2, AircraftStatus.InFlight, 500, 800, 25000, 4.2, 20000, 5000); // cargo
            var plane3 = new PrivateAircraft(3, AircraftStatus.InFlight, 200, 700, 10000, 2.1, 8000, "Moisés Martínez"); // private

            plane1.ShowInfo(); // calling the function used on each subclass
            plane2.ShowInfo();
            plane3.ShowInfo();

            airp
        }
    }
}
