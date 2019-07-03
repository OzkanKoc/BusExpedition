namespace VoyageFramework
{
    public abstract class Bus
    {
        public Bus(string make, bool hasToilet)
            : this(make, string.Empty, hasToilet)
        { }
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
        public abstract SeatInformation GetSeatInformation(int seatNumber);
    }
}