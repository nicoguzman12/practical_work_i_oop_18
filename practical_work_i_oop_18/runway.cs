using System;

namespace AirUFV
{
    public abstract class Runway //creating abstract class
    {
        public string Id { get; set; } //{get;set} to encapsulate the properties and do the code cleaner
        public RunwayStatus Status { get; set; }
        public Aircraft? CurrentAircraft { get; set; }
        public int TicksAvailability { get; set; } = 3; 
        public Runway(string id, RunwayStatus status) // construction of the class
        {
            Id = id;
            Status = status;
            CurrentAircraft = null; //initialize as null because until some aircraft land, the runway is empty 
        }

        public bool RequestRunway(Aircraft aircraft) 
        {
            if (Status == RunwayStatus.Free) //If runway is available
            {
                CurrentAircraft = aircraft; //assign the aircraft to the runway
                Status = RunwayStatus.occupied; 
                return true; 
            }
            return false; //If runway is not free, you can't land
        }

        public void ReleaseRunway()
        {
            CurrentAircraft = null; //to clear the current aircraft
            Status = RunwayStatus.Free; //runway status back to free
        }
    }
}
