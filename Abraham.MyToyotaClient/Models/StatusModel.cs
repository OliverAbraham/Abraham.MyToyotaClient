namespace Abraham.MyToyotaClient.Models
{
    public class RealtimeStatus
    {
        public RealtimeStatusStatus status { get; set; }
        public RealtimeStatusPayload payload { get; set; }
    }

    public class RealtimeStatusStatus
    {
        public object serviceIdentifier { get; set; }
        public List<RealtimeStatusMessage> messages { get; set; }
    }

    public class RealtimeStatusMessage
    {
        public string responseCode { get; set; }
        public string description { get; set; }
        public string detailedDescription { get; set; }
    }

    public class RealtimeStatusPayload
    {
        public string appRequestNo { get; set; }
        public string returnCode { get; set; }
    }
}
