namespace VoyageFramework
{
    public class Route
    {
        private const int ThresholdOfDistance = 200;
        private const int SecondsPerKilometer = 45;
        private const int MinutesPerBreak = 30;
        private const int Level01Distance = 300;
        private const int DistanceStep = 25;
        private const double Level01PriceFactor = 5;
        private const double Level02PriceFactor = 4.25;

        private int _breakCount;
        private double _basePrice;

        private int MaximumBreakCount => Distance / ThresholdOfDistance;
        private int MinumumBreakCount => 0;

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
                return BreakCount > 0
                    ? $"{DepartureLocation} - {ArrivalLocation} / {Distance} KM'lik {BreakCount} molalı rota"
                    : $"{DepartureLocation} - {ArrivalLocation} / {Distance} KM'lik express rota";
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
                return unrestrictedTime + BreakCount * MinutesPerBreak;
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
                if (value < MinumumBreakCount)
                {
                    _breakCount = MinumumBreakCount;
                }
                if (value >= MinumumBreakCount)
                {
                    _breakCount = MaximumBreakCount;
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
                if (Distance <= Level01Distance)
                {
                    _basePrice = Distance / DistanceStep * Level01PriceFactor;
                    if (Distance % DistanceStep != 0)
                    {
                        _basePrice += Level01PriceFactor;
                    }
                }
                else
                {
                    _basePrice = Level01Distance / DistanceStep * Level01PriceFactor;
                    _basePrice += (Distance - Level01Distance) / DistanceStep * Level02PriceFactor;
                    if ((Distance - Level01Distance) % DistanceStep != 0)
                    {
                        _basePrice += Level02PriceFactor;
                    }
                }
                return (decimal)_basePrice;
            }
        }
    }
}