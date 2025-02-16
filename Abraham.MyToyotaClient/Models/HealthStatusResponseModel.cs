namespace MyToyotaClient.Models
{
    public class HealthStatusResponseModel
    {
        public string status { get; set; }
        public int code { get; set; }
        public List<object> errors { get; set; }
        public HealthPayload payload { get; set; }
    }

    public class HealthPayload
    {
        public List<object> quantityOfEngOilIcon { get; set; }
        public List<object> warning { get; set; }
        public DateTime wnglastUpdTime { get; set; }
        public string vin { get; set; }
    }
}
