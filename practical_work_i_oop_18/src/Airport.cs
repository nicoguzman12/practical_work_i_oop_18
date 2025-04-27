using System;
using System.Collections.Generic;
using System.IO;

namespace AirUFV
{
    public class Airport
    {
        public Runway[,] Runways { get; set; } // 2D array of runways
        public List<Aircraft> Aircrafts { get; set; } // all aircrafts currently managed by the airport

        public Airport(int runwayCount) // Constructor
        {
            Runways = new Runway[runwayCount, 1];
            Aircrafts = new List<Aircraft>();

            for (int i = 0; i < runwayCount; i++)
            {
                Runways[i, 0] = new Runway($"Runway-{i + 1}", RunwayStatus.Free);
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine("Airport Status:");
            foreach (var runway in Runways)
            {
                Console.WriteLine($"Runway {runway.Id}: {runway.Status}");
                if (runway.CurrentAircraft != null)
                {
                    Console.WriteLine($"Aircraft on Runway: {runway.CurrentAircraft.Id}");
                }
            }

            Console.WriteLine("Aircrafts at the airport:");
            foreach (var aircraft in Aircrafts)
            {
                aircraft.ShowInfo();
            }
        }

        public void AdvanceTick()
        {
            foreach (var aircraft in Aircrafts)
            {
                if (aircraft.Status == AircraftStatus.InFlight)
                {
                    aircraft.UpdateDistanceAndFuel(0.25);
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
                    aircraft.Status = AircraftStatus.OnGround;
                    foreach (var runway in Runways)
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
            if (!File.Exists(filePath))
            {
                Console.WriteLine("ERROR: File not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    var parts = lines[i].Split(',');

                    int id = int.Parse(parts[0]);
                    AircraftStatus status = Enum.Parse<AircraftStatus>(parts[1]);
                    int distance = int.Parse(parts[2]);
                    int speed = int.Parse(parts[3]);
                    string type = parts[4];
                    double fuelCapacity = double.Parse(parts[5]);
                    double fuelConsumption = double.Parse(parts[6]);
                    string extra = parts[7];

                    Aircraft aircraft;

                    switch (type)
                    {
                        case "Commercial":
                            int passengers = int.Parse(extra);
                            aircraft = new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, fuelCapacity, passengers);
                            break;

                        case "Cargo":
                            double load = double.Parse(extra);
                            aircraft = new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, fuelCapacity, load);
                            break;

                        case "Private":
                            aircraft = new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, fuelCapacity, extra);
                            break;

                        default:
                            Console.WriteLine($"Unknown aircraft type on line {i + 1}. Skipping this entry.");
                            continue;
                    }

                    Aircrafts.Add(aircraft);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
                }
            }
        }
    }
}




