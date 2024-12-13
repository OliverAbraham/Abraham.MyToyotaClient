namespace MyToyotaClient.Models
{
    public class LocationResponseModel
    {
        public LocationPayload payload { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<object> errors { get; set; }
    }

    public class LocationPayload
    {
        public string vin { get; set; }
        public DateTime lastTimestamp { get; set; }
        public VehicleLocation vehicleLocation { get; set; }
    }

    public class VehicleLocation
    {
        public string displayName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime locationAcquisitionDatetime { get; set; }
    }
}
