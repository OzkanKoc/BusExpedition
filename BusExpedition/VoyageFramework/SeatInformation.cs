namespace VoyageFramework
{
    public struct SeatInformation
    {
        public SeatInformation(int number, SeatSection section, SeatCategory category)
        {
            Number = number;
            Section = section;
            Category = category;
        }
        public int Number { get; }
        public SeatSection Section { get; }
        public SeatCategory Category { get; }
    }
}