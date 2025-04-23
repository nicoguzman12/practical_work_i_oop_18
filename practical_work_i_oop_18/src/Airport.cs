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
    }
}
