namespace MyToyotaClient.Models;

public class AccessTokenRequest
{
    public string client_id { get; set; }
    public List<string> code { get; set; }
    public string redirect_uri { get; set; }
    public string grant_type { get; set; }
    public string code_verifier { get; set; }
}
