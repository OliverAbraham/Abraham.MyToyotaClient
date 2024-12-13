namespace MyToyotaClient.Models
{
    public class TelemetryStatusResponseModel
    {
        public TelemetryPayload payload { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<object> errors { get; set; }
    }

    public class TelemetryPayload
    {
        public string fuelType { get; set; }
        public Odometer odometer { get; set; }
        public int batteryLevel { get; set; }
        public DistanceToEmpty distanceToEmpty { get; set; }
        public DateTime timestamp { get; set; }
        public string chargingStatus { get; set; }
    }

    public class DistanceToEmpty
    {
        public int value { get; set; }
        public string unit { get; set; }
    }

    public class Odometer
    {
        public int value { get; set; }
        public string unit { get; set; }
    }
}
