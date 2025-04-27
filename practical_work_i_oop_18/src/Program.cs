using System;

namespace AirUFV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create an instance of the airport with 2 runways
            Airport airport = new Airport(2);
            string filePath = "../../../Aircraft_Status.csv";

            bool running = true;
            while (running)
            {
                //show the menu
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Load flights from file.");
                Console.WriteLine("2. Load a flight manually.");
                Console.WriteLine("3. Start simulation (Manual).");
                Console.WriteLine("4. Exit.");
                Console.WriteLine("\n -------------------------------");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter the file name: (Airport_Status.csv) ");
                        airport.LoadAircraftFromFile(filePath);
                        break;
                    case "2":
                        Console.WriteLine("\n--- Load a flight manually ---");
                        Console.WriteLine("Choose aircraft type:");
                        Console.WriteLine("1. Commercial");
                        Console.WriteLine("2. Cargo");
                        Console.WriteLine("3. Private");
                        Console.Write("Enter aircraft type: ");
                        string? writtenChoice = Console.ReadLine();

                        if (writtenChoice == "1" || writtenChoice == "2" || writtenChoice == "3")
                        {
                            try // specific exceptions are monitored
                            {
                                Console.Write("Enter aircraft ID: ");
                                int id = int.Parse(Console.ReadLine() ?? "0"); // ?? "0" for null values
                                Console.Write("Enter initial Distance: ");
                                int distance = int.Parse(Console.ReadLine() ?? "0");
                                Console.Write("Enter speed: ");
                                int speed = int.Parse(Console.ReadLine() ?? "0");
                                Console.Write("Enter fuel capacity: ");
                                double fuelCapacity = double.Parse(Console.ReadLine() ?? "0");
                                Console.Write("Enter fuel consumption: ");
                                double fuelConsumption = double.Parse(Console.ReadLine() ?? "0");
                                double currentFuel = fuelCapacity; //assuming full fuel on manual load

                                switch (writtenChoice)
                                {
                                    case "1":
                                        Console.Write("Enter the number of passengers: ");
                                        int passengers = int.Parse(Console.ReadLine() ?? "0");
                                        airport.Aircrafts.Add(new CommercialAircraft(id, AircraftStatus.InFlight, distance, speed, fuelCapacity, fuelConsumption, currentFuel, passengers));
                                        Console.WriteLine("Commercial aircraft added.");
                                        break;
                                    case "2":
                                        Console.Write("Enter max load: ");
                                        double load = double.Parse(Console.ReadLine() ?? "0");
                                        airport.Aircrafts.Add(new CargoAircraft(id, AircraftStatus.InFlight, distance, speed, fuelCapacity, fuelConsumption, currentFuel, load));
                                        Console.WriteLine("Cargo aircraft added.");
                                        break;
                                    case "3":
                                        Console.Write("Enter owner's name: ");
                                        string? ownerName = Console.ReadLine();
                                        if (ownerName == null)
                                        {
                                            
                                            Console.WriteLine("The aircraft has to have an owner.");
                                        }
                                        else
                                        {
                                            airport.Aircrafts.Add(new PrivateAircraft(id, AircraftStatus.InFlight, distance, speed, fuelCapacity, fuelConsumption, currentFuel, ownerName));
                                            Console.WriteLine("Private aircraft added.");
                                        }
                                        break;
                                }
                            }
                            catch (FormatException) //catches the exception thrown by the try block
                            {
                                Console.WriteLine("Invalid input format. Please enter numbers where required.");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid aircraft type choice.");
                        }
                        break;
                  case "3":
                        // Lógica para iniciar la simulación manual
                        Console.WriteLine("\n--- Starting Simulation (Manual) ---");
                        if (airport.Aircrafts.Count == 0)
                        {
                            Console.WriteLine("No aircraft loaded. Please load flights first.");
                        }
                        else
                        {
                            bool anyAircraftInAir = false;
                            foreach (var aircraft in airport.Aircrafts)
                            {
                                if (aircraft.Status != AircraftStatus.OnGround)
                                {
                                    anyAircraftInAir = true;
                                    break;
                                }
                            }
                            while (anyAircraftInAir)
                            {
                                airport.ShowStatus();
                                Console.WriteLine("\nPress any key to advance the simulation tick");
                                Console.ReadKey();
                                airport.AdvanceTick();

                                anyAircraftInAir = false;
                                foreach (var aircraft in airport.Aircrafts)
                                {
                                    if (aircraft.Status != AircraftStatus.OnGround)
                                    {
                                        anyAircraftInAir = true;
                                        break;
                                    }
                                }
                            }
                            Console.WriteLine("\nSimulation complete. All aircraft have landed.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exiting program.");
                        running = false;
                        break;
                }
            }

            Console.WriteLine("\nProgram finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}