using System;

namespace AirUFV
{
    public class Airport
    {

        public Runway[,] Runways { get; set; }

        public List<Aircraft> Aircrafts {get; set; }

        public Airport(int runwayCount) //Constructor
        {
            Runways = new Runway[runwayCount, 1];
            Aircrafts = new List<Aircraft>();

            // Initialize each runway and set its status to Free            
            for (int i = 0; i< runwayCount; i++)
            {
                Runways[i, 0] = new Runway($"Runway-{i + 1}", RunwayStatus.Free);
        
            }
        }

        public void ShowStatus() // Display the current status
        {
            Console.WriteLine("Airport Status:");

            foreach (var runway in Runways)
            {
                Console.WriteLine($"Runway {runway.Id}: {runway.Status}");

                if (runway.CurrentAircraft != null) // If a runway currently has an aircraft, display the aircraft ID
                {
                    Console.WriteLine($"    Aircraft on Runway: {runway.CurrentAircraft.Id}");
                }
            }
        }
            public void AdvanceTick() // Advance the airport state by one tick (15 minutes interval)
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
    }
}
