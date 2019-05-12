namespace VoyageFramework
{
    public class LuxuryBus : Bus
    {
        private int _capacity = 30;

        public LuxuryBus(string make, string plate) :
            base(make, plate, true)
        { }

        public override int Capacity { get => _capacity; }

        public override SeatInformation GetSeatInformation(int seatNumber)
        {
            SeatInformation seatInformation;
            switch (seatNumber % 2)
            {
                case 1:
                    seatInformation = new SeatInformation(seatNumber, SeatSection.LeftSide, SeatCategory.Singular);
                    break;
                case 0:
                    seatInformation = new SeatInformation(seatNumber, SeatSection.RightSide, SeatCategory.Singular);
                    break;
                default:
                    seatInformation = new SeatInformation();
                    break;
            }
        
            return seatInformation;
        }
    }
}