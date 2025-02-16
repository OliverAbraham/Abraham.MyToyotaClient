namespace MyToyotaClient.Models
{
    public class RemoteStatusResponseModel
    {
        public Status status { get; set; }
        public RemoteStatusPayload payload { get; set; }
    }

    public class RemoteStatusPayload
    {
        public List<VehicleStatus> vehicleStatus { get; set; }
        public Telemetry telemetry { get; set; }
        public DateTime occurrenceDate { get; set; }
        public int cautionOverallCount { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime locationAcquisitionDatetime { get; set; }
    }

    public class VehicleStatus
    {
        public string category { get; set; }
        public int displayOrder { get; set; }
        public List<Section> sections { get; set; }
    }

    public class Section
    {
        public string section { get; set; }
        public List<SectionValue> values { get; set; }
    }

    public class SectionValue
    {
        public string value { get; set; }
        public int status { get; set; }
    }

    public class Telemetry
    {
        public Fugage fugage { get; set; }
        public Rage rage { get; set; }
        public Odo odo { get; set; }
    }

    public class Fugage
    {
        public object value { get; set; }
        public object unit { get; set; }
    }

    public class Rage
    {
        public object value { get; set; }
        public object unit { get; set; }
    }

    public class Odo
    {
        public double value { get; set; }
        public string unit { get; set; }
    }
}
