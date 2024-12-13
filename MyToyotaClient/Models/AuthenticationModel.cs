namespace MyToyotaClient.Models;

public class Callback
{
    public string type { get; set; }
    public List<Output> output { get; set; }
    public List<Input> input { get; set; }
    public int _id { get; set; }
}

public class Input
{
    public string name { get; set; }
    public object value { get; set; }
}

public class Output
{
    public string name { get; set; }
    public object value { get; set; }
}

public class AuthenticationModel
{
    public string authId { get; set; }
    public List<Callback> callbacks { get; set; }
}

public class AuthenticationModel2
{
    public string tokenId { get; set; }
    public string successUrl { get; set; }
    public string realm { get; set; }
}

