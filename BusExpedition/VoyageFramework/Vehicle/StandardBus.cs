namespace VoyageFramework
{
    public class StandardBus : Bus
    {
        private int _capacity = 30;

        public StandardBus(string make, string plate) :
            base(make, plate, false)
        { }

        public override int Capacity { get => _capacity; }

        public override SeatInformation GetSeatInformation(int seatNumber)
        {
            SeatInformation seatInformation;
            switch (seatNumber % 3)
            {
                case 1:
                    seatInformation = new SeatInformation(seatNumber, SeatSection.LeftSide, SeatCategory.Singular);
                    break;
                case 2:
                    seatInformation = new SeatInformation(seatNumber, SeatSection.RightSide, SeatCategory.Corridor);
                    break;
                case 0:
                    seatInformation = new SeatInformation(seatNumber, SeatSection.RightSide, SeatCategory.Window);
                    break;
                default:
                    seatInformation = new SeatInformation();
                    break;
            }

            return seatInformation;
        }
    }
}