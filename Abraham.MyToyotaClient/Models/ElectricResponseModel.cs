namespace MyToyotaClient.Models
{
    public class ElectricResponseModel
    {
        public Status status { get; set; }
        public Payload payload { get; set; }
    }

    public class Payload
    {
        public int batteryLevel { get; set; }
        public string chargingStatus { get; set; }
        public EvRange evRange { get; set; }
        public EvRangeWithAc evRangeWithAc { get; set; }
        public DateTime lastUpdateTimestamp { get; set; }
    }

    public class EvRange
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class EvRangeWithAc
    {
        public string unit { get; set; }
        public double value { get; set; }
    }
}
