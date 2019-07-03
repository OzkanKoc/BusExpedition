namespace VoyageFramework
{
    public class LuxuryBus : Bus
    {
        private const bool LuxuryBusHasRoilet = true;
        private const int LuxuryBusCapacity = 20;

        public LuxuryBus(string make, string plate) :
            base(make, plate, LuxuryBusHasRoilet)
        { }

        public override int Capacity { get => LuxuryBusCapacity; }

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