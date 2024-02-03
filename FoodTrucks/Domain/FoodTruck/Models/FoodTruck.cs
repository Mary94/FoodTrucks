namespace FoodTrucks.Domain.FoodTruck
{
    public class FoodTruck
    {
        public int LocationId { get; set; }
        public string? Applicant { get; set; }
        public string? FacilityType { get; set; }
        public int? CNN { get; set; }
        public string? LocationDescription { get; set; }
        public string? Address { get; set; }
        public string? BlockLot { get; set; }
        public string? Block { get; set; }
        public string? Lot { get; set; }
        public string? Permit { get; set; }
        public string? Status { get; set; }
        public string? FoodItems { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Schedule { get; set; }
        public string? DaysHours { get; set; }
        public string? NOISent { get; set; }
        public string? Approved { get; set; }
        public string? Received { get; set; }
        public string? PriorPermit { get; set; }
        public string? ExpirationDate { get; set; }
    }
}
