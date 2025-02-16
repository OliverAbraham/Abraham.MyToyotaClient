namespace MyToyotaClient.Models
{
    internal class AccessTokenDTO
    {
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
        public string grant_type { get; set; }
        public string code_verifier { get; set; }
        public string refresh_token { get; set; }
    }
}