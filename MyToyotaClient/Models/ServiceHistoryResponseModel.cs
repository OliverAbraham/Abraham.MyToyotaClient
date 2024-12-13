namespace MyToyotaClient.Models
{
    public class ServiceHistoryResponseModel
    {
        public Status status { get; set; }
        public ServiceHistoryPayload payload { get; set; }
    }

    public class ServiceHistoryPayload
    {
        public List<ServiceHistory> serviceHistories { get; set; }
    }

    public class ServiceHistory
    {
        public string serviceDate { get; set; }
        public object servicingDealer { get; set; }
        public string mileage { get; set; }
        public string unit { get; set; }
        public object roNumber { get; set; }
        public object operationsPerformed { get; set; }
        public string serviceProvider { get; set; }
        public object notes { get; set; }
        public string serviceHistoryId { get; set; }
        public string serviceCategory { get; set; }
        public bool customerCreatedRecord { get; set; }
    }
}
