using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Ticket
    {
        internal Ticket(BusExpedition expedition, SeatInformation seatInformation, Person passenger, decimal paidAmount)
        {
            Expedition = expedition;
            SeatInformation = seatInformation;
            Passenger = passenger;
            PaidAmount = paidAmount;
        }
        public BusExpedition Expedition { get; }
        public SeatInformation SeatInformation { get; }
        public Person Passenger { get; }
        public decimal PaidAmount { get; }


    }
}
