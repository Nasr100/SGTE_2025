namespace Route_Service.Models
{
    public class RouteSolution
    {
        public List<Location> OrderedLocations { get; set; } = new List<Location>();
        public List<long> NodeArrivalTimes { get; set; } = new List<long>();
        public long TotalDurationMinutes { get; set; }
    }
}
