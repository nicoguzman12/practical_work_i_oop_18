using System;

namespace AirUFV
{
    public class Airport
    {

        public Runway[,] Runways { get; set; } //2d array of runways

        public List<Aircraft> Aircrafts {get; set; } //all aircrafts curently managed by the airport

        public Airport(int runwayCount) //Constructor
        {
            Runways = new Runway[runwayCount, 1];
            Aircrafts = new List<Aircraft>();

            //initialize the runways and start in free status            
            for (int i = 0; i< runwayCount; i++)
            {
                Runways[i, 0] = new Runway($"Runway-{i + 1}", RunwayStatus.Free);
        
            }
        }

        public void ShowStatus() //display the current status
        {
            Console.WriteLine("Airport Status:");

            foreach (var runway in Runways)
            {
                Console.WriteLine($"Runway {runway.Id}: {runway.Status}");

                if (runway.CurrentAircraft != null) //if a runway currently has an aircraft shows wich aircraft is on it
                {
                    Console.WriteLine($"Aircraft on Runway: {runway.CurrentAircraft.Id}");
                }
            }

            Console.WriteLine("Aircrafts at the airport:");
            foreach (var aircraft in Aircrafts)
            {
                aircraft.ShowInfo(); //Show each aircraft infromation
            }
        }
            public void AdvanceTick() //advance the airport state 15 mins by one tick 
            {
                foreach (var aircraft in Aircrafts)
                {
                    if (aircraft.Status == AircraftStatus.InFlight)
                    {
                        aircraft.UpdateDistanceAndFuel(0.25); //15 mins = 0.25 hours
                    }

                    else if (aircraft.Status == AircraftStatus.Waiting)
                    {

                        foreach (var runway in Runways)
                        {
                            if (runway.RequestRunway(aircraft))
                            {
                                aircraft.Status = AircraftStatus.Landing;
                                break;
                            }
                        }
                    }

                    else if (aircraft.Status == AircraftStatus.Landing)
                    {
                        
                        aircraft.Status = AircraftStatus.OnGround; //Transition from landing to be on the ground

                        foreach (var runway in Runways) //Release the runway
                        {
                            if (runway.CurrentAircraft == aircraft)
                            {
                                runway.ReleaseRunway();
                            }
                        }
                    }
                }
            }

        public void LoadAircraftFromFile(string filePath)

        {

        // Check if the file exists at the given path
        if (!File.Exists(filePath))
        {
            Console.WriteLine("ERROR: File not found.");
            return; // Exit the method if the file doesn't sexist
        }

        // Read all lines from the file (each line represents one aircraft)
        string[] lines = File.ReadAllLines(filePath);

        // Start at index 1 to skip the header line
        for (int i = 1; i < lines.Length; i++)
        {
            do
            {
                // Split the line into parts using comma as the delimiter
                var parts = lines[i].Split(',');

                // Parse the common fields for all aircraft
                int id = int.Parse(parts[0]); 
                AircraftStatus status = Enum.Parse<AircraftStatus>(parts[1]); 
                int distance = int.Parse(parts[2]); 
                int speed = int.Parse(parts[3]); 
                string type = parts[4]; 
                double fuelCapacity = double.Parse(parts[5]); 
                double fuelConsumption = double.Parse(parts[6]); 
                string extra = parts[7]; 

                Aircraft aircraft;

                // Create the appropriate aircraft object based on its type
                switch (type)
                {
                    case "Commercial":
                        int passengers = int.Parse(extra); // Number of passengers
                        aircraft = new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, fuelCapacity, passengers);
                        break;

                    case "Cargo":
                        double load = double.Parse(extra); // Maximum cargo load
                        aircraft = new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, fuelCapacity, load);
                        break;

                    case "Private":
                        aircraft = new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, fuelCapacity, extra); // Extra is the owner's name
                        break;

                    default:
                        Console.WriteLine($"Unknown aircraft type on line {i + 1}. Skipping this entry.");
                        continue; // Skip this line if type is unknown
                }

                // Add the aircraft to the airport's list
                Aircrafts.Add(aircraft);
            }
            catch (Exception ex)
            {
                // Handle any errors (invalid format, wrong data types, etc.) and continue with the rest
                Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
            }
        }
      }
    }
 }




