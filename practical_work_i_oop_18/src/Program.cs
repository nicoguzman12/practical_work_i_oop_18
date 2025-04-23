using System;

namespace AirUFV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create an instance of the airport with 2 runways
            Airport airport = new Airport(2);

            // Ask the user for the path to the CSV file
            Console.Write("Enter the path to the aircraft CSV file: ");
            string Aircraft_Status.csv = Console.ReadLine();

            // Load aircraft from the file into the airport
            airport.LoadAircraftFromFile(Aircraft_Status.csv);

            // Show the current status of the airport
            airport.ShowStatus();


            //Advance the simulation by a few ticks
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\n--- Tick {i + 1} ---");
                airport.AdvanceTick();
                airport.ShowStatus();
            }

            // Wait for user input to close
            Console.WriteLine("\nSimulation complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
