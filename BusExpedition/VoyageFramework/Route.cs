namespace VoyageFramework
{
    public class Route
    {
        private const int ThresholdDistance = 200;
        private const int SecondsPerKilometer = 45;
        private const int SecondsPerBreak = 30;
        private int _breakCount;
        private double _basePrice;

        public Route(string departureLocation, string arrivalLocation, int distance)
        {
            DepartureLocation = departureLocation;
            ArrivalLocation = arrivalLocation;
            Distance = distance;
        }

        public string Name
        {
            get
            {
                if (BreakCount > 0)
                {
                    return string.Format("{0} - {1} / {2} KM'lik {3} molalı rota ", DepartureLocation, ArrivalLocation, Distance, BreakCount);
                }
                else
                {
                    return string.Format("{0} - {1} / {2} KM'lik Express rota ", DepartureLocation, ArrivalLocation, Distance);
                }

            }
        }
        public string DepartureLocation { get; }
        public string ArrivalLocation { get; }
        public int Distance { get; }
        public int Duration
        {
            get
            {
                int totalSeconds = Distance * SecondsPerKilometer;
                int unrestrictedTime = totalSeconds % 60 == 0 ? totalSeconds / 60 : totalSeconds / 60 + 1;
                return unrestrictedTime + BreakCount % 2 == 0 ? BreakCount * SecondsPerBreak / 60 : BreakCount * 30 / 60 + 1;
            }
        }
        public int BreakCount
        {
            get
            {
                return _breakCount;
            }
            set
            {
                if (Distance % ThresholdDistance > value)
                {
                    _breakCount = Distance % 200;
                }
                else if (Distance < ThresholdDistance)
                {
                    _breakCount = 0;
                }
                else
                {
                    _breakCount = value;
                }
            }
        }
        public decimal BasePrice
        {
            get
            {
                if (Distance <= 300)
                {
                    _basePrice = Distance / 25 * 5.0;
                    if (Distance % 25 != 0)
                    {
                        _basePrice += 5;
                    }
                }
                else
                {
                    _basePrice = 300 / 25 * 5.0;
                    _basePrice += ((Distance - 300) / 25 * 4.25);
                    if ((Distance - 300) % 25 != 0)
                    {
                        _basePrice += 4.25;
                    }
                }
                return (decimal)_basePrice;
            }
        }
    }
}