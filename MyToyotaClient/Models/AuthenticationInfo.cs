namespace MyToyotaClient.Models;

internal class AuthenticationInfo
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string uuid { get; set; }
    public string expiration { get; set; }
    public string username { get; set; }
}

