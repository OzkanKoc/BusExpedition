namespace VoyageFramework
{
    abstract public class Bus
    {
        public Bus(string make, string plate, bool hasToilet)
        {
            Make = make;
            Plate = plate;
            HasToilet = hasToilet;
        }
        public string Make { get; }
        public string Plate { get; set; }
        public abstract int Capacity { get; }
        public bool HasToilet { get; }
        abstract public SeatInformation GetSeatInformation(int seatNumber); 
    }
}