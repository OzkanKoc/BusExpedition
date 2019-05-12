using System;
using VoyageFramework.Collections;

namespace VoyageFramework
{
    public class BusExpedition
    {
        private DateTime _estimatedDepartureTime;
        private Bus _bus;
        private HostCollection _hosts = new HostCollection();
        private DriverCollection _drivers = new DriverCollection();
        private TicketCollection _tickets = new TicketCollection();

        public BusExpedition(Route route, DateTime departureTime)
        {
            DepartureTime = departureTime;
            Route = route;
            EstimatedDepartureTime = departureTime;

        }

        public string Code
        {
            get
            {
                Random rnd = new Random();

                return string.Format("{{{0}}} {{{1}}} - {2} - {3}",
                    Route.DepartureLocation.Substring(0, 1),
                    DepartureTime.ToString("yyMMdd"),
                    Bus.GetType() == typeof(LuxuryBus) ? "LX" : "ST",
                    rnd.Next(1000, 100000));
            }
        }
        public Bus Bus
        {
            get => _bus;
            set
            {
                var bus = value;
                if (_drivers.Count > 0)
                {
                    for (int i = 0; i < _drivers.Count; i++)
                    {
                        if ((bus is LuxuryBus && _drivers[i].LicenseType != LicenseType.HighLicense) ||
                            (bus is StandardBus && _drivers[i].LicenseType == LicenseType.None))
                        {
                            throw new FormatException(_drivers[i].LicenseType.ToString());
                        }
                        else
                        {
                            _bus = value;
                        }
                    }
                }
                else
                {
                    _bus = value;
                }
            }
        }
        public Route Route { get; }
        public DriverCollection Drivers
        {
            get
            {
                return _drivers;
            }
        }
        public HostCollection Hosts
        {
            get
            {
                return _hosts;
            }
        }
        private TicketCollection Tickets
        {
            get
            {
                return _tickets;
            }
        }
        public DateTime DepartureTime { get; }
        public DateTime EstimatedDepartureTime
        {
            get
            {
                return _estimatedDepartureTime;
            }
            set
            {
                if (value < DepartureTime)
                {
                    _estimatedDepartureTime = DepartureTime;
                }
                else
                {
                    _estimatedDepartureTime = value;
                }
            }
        }
        public DateTime EstimatedArrivalTime
        {
            get
            {
                return EstimatedDepartureTime.AddMinutes(Route.Duration);
            }
        }
        public bool HasDelay
        {
            get
            {
                return DepartureTime < EstimatedDepartureTime;
            }
        }
        public bool HasSnackService { get; set; }



        private bool IsAvailableForSelling(Person person, int seatNumber, decimal fee)
        {
            return IsSeatEmpty(seatNumber) && ((IsEmployee(person) && fee >= Route.BasePrice)
                                                || (fee >= Route.BasePrice * 1.05m))
                   && IsSeatAvailableFor(seatNumber, person.Gender);

        }
        private bool IsAvailableForDoubleSelling(Person[] people, int seatNumber, decimal fee)
        {
            return IsSeatEmpty(seatNumber) && IsSeatEmpty(seatNumber + 1) &&
                    ((IsEmployee(people[0]) && IsEmployee(people[1]) && fee >= Route.BasePrice) ||
                    fee >= Route.BasePrice * 1.05m);
        }

        private bool IsEmployee(Person person)
        {
            return person is Driver || person is Host;
        }

        private Gender GetGender(int seatNum)
        {
            for (int i = 0; i < _tickets.Count; i++)
            {
                if (seatNum == _tickets[i].SeatInformation.Number)
                {
                    return _tickets[i].Passenger.Gender;
                }
            }

            return Gender.None;
        }

        public void AddDriver(Driver driver)
        {
            if ((Bus is LuxuryBus && driver.LicenseType != LicenseType.HighLicense) ||
                Bus is StandardBus && driver.LicenseType == LicenseType.None)
            {
                throw new FormatException(nameof(driver.LicenseType));
            }
            else
            {
                if (_drivers.Count < (int)Math.Ceiling((decimal)Route.Distance / 400) + 1)
                {
                    _drivers.Add(driver);
                }
            }

        }

        public void AddHost(Host host)
        {
            _hosts.Add(host);
        }

        public void RemoveDriver(Driver driver)
        {
            if (_drivers.Contains(driver))
            {
                _drivers.Remove(driver);
            }
        }

        public void RemoveHost(Host host)
        {
            if (_hosts.Contains(host))
            {
                _hosts.Remove(host);
            }
        }

        public decimal GetPriceOf(int seatNumber)
        {
            decimal price;
            if (Bus.GetType() == typeof(LuxuryBus))
            {
                price = Route.BasePrice * 1.35m;
            }
            else
            {
                price = seatNumber % 3 == 1 ? Route.BasePrice * 1.25m : price = Route.BasePrice * 1.2m;
            }
            return price;
        }

        public Ticket SellTicket(Person person, int seatNumber, decimal fee)
        {
            Ticket ticket = null;
            if (IsAvailableForSelling(person, seatNumber, fee))
            {
                ticket = new Ticket(this, GetSeatInformation(seatNumber), person, fee);
                _tickets.Add(ticket);
            }

            return ticket;
        }

        public Ticket[] SellDoubleTickets(Person person01, Person person02, int seatNumber, decimal fee)
        {
            Ticket[] doubleTickets = new Ticket[2];

            if (IsAvailableForDoubleSelling(new Person[2] { person01, person02 }, seatNumber, fee))
            {
                doubleTickets[0] = new Ticket(this, GetSeatInformation(seatNumber), person01, fee);
                doubleTickets[1] = new Ticket(this, GetSeatInformation(seatNumber + 1), person02, fee);
                _tickets.Add(doubleTickets[0]);
                _tickets.Add(doubleTickets[1]);
            }
            return doubleTickets;
        }

        public void CancelTicket(Ticket ticket)
        {
            if (_tickets.Contains(ticket))
            {
                _tickets.Remove(ticket);
            }
        }

        public bool IsSeatEmpty(int seatNumber)
        {
            for (int i = 0; i < _tickets.Count; i++)
            {
                if (_tickets[i].SeatInformation.Number == seatNumber)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSeatAvailableFor(int seatNumber, Gender gender)
        {
            if (GetSeatInformation(seatNumber).Category == SeatCategory.Corridor)
            {
                if (!IsSeatEmpty(seatNumber + 1))
                {
                    return GetGender(seatNumber + 1) == gender;
                }
                else
                {
                    return true;
                }
            }
            else if (GetSeatInformation(seatNumber).Category == SeatCategory.Window)
            {
                if (!IsSeatEmpty(seatNumber - 1))
                {
                    return GetGender(seatNumber - 1) == gender;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public SeatInformation GetSeatInformation(int seatNumber)
        {
            return Bus.GetSeatInformation(seatNumber);
        }
    }
}
