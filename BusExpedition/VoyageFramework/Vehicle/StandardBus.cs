using System;

namespace VoyageFramework
{
    public class StandardBus : Bus
    {
        private const bool HasNoToilet = false;
        private const int StandartBusCapacity = 30;

        public StandardBus(string make, string plate) :
            base(make, plate, HasNoToilet)
        { }

        public override int Capacity { get => StandartBusCapacity; }

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
                    throw new ArgumentOutOfRangeException(nameof(seatNumber));
            }

            return seatInformation;
        }
    }
}