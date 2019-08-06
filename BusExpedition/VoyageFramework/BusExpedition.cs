using System;
using VoyageFramework.Collection;

namespace VoyageFramework
{
    public class BusExpedition
    {
        private const string CodeOfLuxuryBus = "LX";
        private const string CodeOfStandartBus = "ST";
        private const int MinSuffixValue = 1000;
        private const int MaxSuffixValue = 10000;
        private const int DistancePerDriver = 400;
        private int _codeSuffix;
        private DateTime _estimatedDepartureTime;
        private Bus _bus;

        public BusExpedition(Route route, DateTime departureTime)
        {
            DepartureTime = departureTime;
            Route = route;
            EstimatedDepartureTime = departureTime;
            Drivers = new ListCollection<Driver>();
            Hosts = new ListCollection<Host>();
            Tickets = new ListCollection<Ticket>();

            var random = new Random();
            _codeSuffix = random.Next(MinSuffixValue, MaxSuffixValue);

        }
        public string Code
        {
            get
            {
                return string.Format("{0}{1}-{2}-{3}",
                     Route.DepartureLocation.Substring(0, 1),
                     DepartureTime.ToString("yyMMdd"),
                     Bus is LuxuryBus ? CodeOfLuxuryBus : CodeOfStandartBus,
                    _codeSuffix);
            }
        }
        public Bus Bus
        {
            get => _bus;
            set
            {
                var bus = value;
                if (Drivers.Count > 0)
                {
                    for (int i = 0; i < Drivers.Count; i++)
                    {
                        if ((bus is LuxuryBus && Drivers[i].LicenseType != LicenseType.HighLicense) ||
                            (bus is StandardBus && Drivers[i].LicenseType == LicenseType.None))
                        {
                            throw new FormatException(Drivers[i].LicenseType.ToString());
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
        public ListCollection<Driver> Drivers { get; }
        public ListCollection<Host> Hosts { get; }
        public ListCollection<Ticket> Tickets { get; }
        public Route Route { get; }
        public DateTime DepartureTime { get; }
        public DateTime EstimatedDepartureTime
        {
            get
            {
                return _estimatedDepartureTime < DepartureTime
                    ? DepartureTime
                    : _estimatedDepartureTime;
            }
            set
            {
                _estimatedDepartureTime = value;
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
            return
                IsSeatEmpty(seatNumber) &&
                (
                    (IsEmployee(person) && fee >= Route.BasePrice) ||
                    (fee >= Route.BasePrice * 1.05m)
                ) &&
                IsSeatAvailableFor(seatNumber, person.Gender);
        }
        private bool IsAvailableForDoubleSelling(Person[] people, int seatNumber, decimal fee)
        {
            var nextSeatNumber = GetNextSeatNumber(seatNumber);

            return
                nextSeatNumber > 0 &&
                IsSeatEmpty(seatNumber) &&
                IsSeatEmpty(nextSeatNumber) &&
                (
                    (
                        (IsEmployee(people[0]) || IsEmployee(people[1])) &&
                        fee >= Route.BasePrice * 2
                    )
                    ||
                    fee >= Route.BasePrice * 2.1m // çift kişinin ücretini kontrol ediyoruz
                );
        }

        private int GetNextSeatNumber(int seatNumber)
        {
            var seatInfo = GetSeatInformation(seatNumber);

            int nextSeatNumber;
            if (seatInfo.Category == SeatCategory.Corridor)
            {
                nextSeatNumber = seatNumber + 1;
            }
            else if (seatInfo.Category == SeatCategory.Window)
            {
                nextSeatNumber = seatNumber - 1;
            }
            else
            {
                nextSeatNumber = -1;
            }

            return nextSeatNumber;
        }

        private bool IsEmployee(Person person)
        {
            return person is Driver || person is Host;
        }

        private Gender GetGender(int seatNum)
        {
            for (int i = 0; i < Tickets.Count; i++)
            {
                if (seatNum == Tickets[i].SeatInformation.Number)
                {
                    return Tickets[i].Passenger.Gender;
                }
            }

            return Gender.None;
        }

        public void AddDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }
            else if (
                        (
                            Bus is LuxuryBus &&
                            driver.LicenseType != LicenseType.HighLicense
                        )
                        ||
                        Bus is StandardBus && driver.LicenseType == LicenseType.None
                    )
            {
                throw new
                    InvalidOperationException("Eklenen sürücünün lisans tipi bu araç için uygun değil.");

            }
            else
            {
                var maxDriverAllowed = Math.Ceiling((double)Route.Distance / DistancePerDriver);
                if (Drivers.Count < maxDriverAllowed)
                {
                    Drivers.Add(driver);
                }
            }
        }

        public void AddHost(Host host)
        {
            Hosts.Add(host);
        }

        public void RemoveDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                Drivers.Remove(driver);
            }
        }

        public void RemoveHost(Host host)
        {
            if (Hosts.Contains(host))
            {
                Hosts.Remove(host);
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
                price = seatNumber % 3 == 1
                    ? Route.BasePrice * 1.25m
                    : Route.BasePrice * 1.2m;
            }
            return price;
        }

        public Ticket SellTicket(Person person, int seatNumber, decimal fee)
        {
            Ticket ticket = null;
            if (IsAvailableForSelling(person, seatNumber, fee))
            {
                ticket = new Ticket(this, GetSeatInformation(seatNumber), person, fee);
                Tickets.Add(ticket);
            }

            return ticket;
        }

        public Ticket[] SellDoubleTickets(Person person01, Person person02, int seatNumber, decimal fee)
        {
            Ticket[] doubleTickets = new Ticket[2];
            var persons = new Person[2] { person01, person02 };

            if (IsAvailableForDoubleSelling(persons, seatNumber, fee))
            {
                doubleTickets = new Ticket[2];
                var nextSeatNumber = GetNextSeatNumber(seatNumber);
                doubleTickets[0] = new Ticket(this, GetSeatInformation(seatNumber), person01, fee / 2);
                doubleTickets[1] = new Ticket(this, GetSeatInformation(nextSeatNumber), person02, fee / 2);
                Tickets.Add(doubleTickets[0]);
                Tickets.Add(doubleTickets[1]);
            }
            else
            {
                doubleTickets = new Ticket[0];
            }
            return doubleTickets;
        }

        public void CancelTicket(Ticket ticket)
        {
            if (Tickets.Contains(ticket))
            {
                Tickets.Remove(ticket);
            }
        }

        public bool IsSeatEmpty(int seatNumber)
        {
            for (int i = 0; i < Tickets.Count; i++)
            {
                if (Tickets[i].SeatInformation.Number == seatNumber)
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
                var nextSeatNumber = seatNumber + 1;
                return CheckAvailibilityByNextSeat(nextSeatNumber, gender);
            }
            else if (GetSeatInformation(seatNumber).Category == SeatCategory.Window)
            {
                var nextSeatNumber = seatNumber - 1;
                return CheckAvailibilityByNextSeat(nextSeatNumber, gender);
            }
            else
            {
                return true;
            }
        }

        private bool CheckAvailibilityByNextSeat(int nextSeatNumber, Gender gender)
        {
            return !IsSeatEmpty(nextSeatNumber)
                ? GetGender(nextSeatNumber) == gender
                : true;
        }

        public SeatInformation GetSeatInformation(int seatNumber)
        {
            return Bus.GetSeatInformation(seatNumber);
        }
    }
}
