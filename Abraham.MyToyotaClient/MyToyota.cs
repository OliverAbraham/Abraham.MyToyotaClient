using RestSharp;
using Newtonsoft.Json;
using MyToyotaClient.Models;
using System.Web;
using System.IdentityModel.Tokens.Jwt;

namespace MyToyotaClient;

/// <summary>
/// Connects to Toyota Connected Services API and retrieves data about your Toyota cars.
/// 
/// You can use this Nuget package to read out the current battery level, range or geo position.
/// Please take a look at my demo to find out the individual values.
/// 
/// 
/// ## CREDITS
/// Many thanks to the Toyota Connected Services Europe Python module by Simon Hansen!
/// He has published his client at https://github.com/DurgNomis-drol/mytoyota.
/// 
/// ## LICENSE
/// Licensed under Apache licence.
/// https://www.apache.org/licenses/LICENSE-2.0
/// 
/// ## SOURCE CODE
/// The source code is hosted at:
/// https://github.com/OliverAbraham/Abraham.MyToyotaClient
/// 
/// The Nuget Package is hosted at: 
/// https://www.nuget.org/packages/Abraham.MyToyotaClient
/// 
/// -------------------------------------------------------------------------------------------
/// 
/// 04.05.2025: Took the latest changes "Adopting headers to latest client information" from Simon Hörrles commit:
/// https://github.com/pytoyoda/pytoyoda/commit/ddaec52001a0917f6774b67c177f458a9fc6f535
/// </summary>
public class MyToyota
{
    #region ------------- Toyota Connected Services API constants -----------------------------
    public const string CLIENT_VERSION                                 = "2.14.0";
    
    // API URLs
    public const string API_BASE_URL                                   = "https://ctpa-oneapi.tceu-ctp-prd.toyotaconnectedeurope.io";
    public const string ACCESS_TOKEN_URL                               = "https://b2c-login.toyota-europe.com/oauth2/realms/root/realms/tme/access_token";
    public const string AUTHENTICATE_URL                               = "https://b2c-login.toyota-europe.com/json/realms/root/realms/tme/authenticate?authIndexType=service&authIndexValue=oneapp";
    public const string AUTHORIZE_URL                                  = "https://b2c-login.toyota-europe.com/oauth2/realms/root/realms/tme/authorize?client_id=oneapp&scope=openid+profile+write&response_type=code&redirect_uri=com.toyota.oneapp:/oauth2Callback&code_challenge=plain&code_challenge_method=plain";
    public const string API_KEY                                        = "tTZipv6liF74PwMfk9Ed68AQ0bISswwf3iHQdqcF";

    // Endpoint URLs
    public const string VEHICLE_GUID_ENDPOINT                          = "/v2/vehicle/guid";
    public const string VEHICLE_LOCATION_ENDPOINT                      = "/v1/location";
    public const string VEHICLE_HEALTH_STATUS_ENDPOINT                 = "/v1/vehiclehealth/status";
    public const string VEHICLE_GLOBAL_REMOTE_STATUS_ENDPOINT          = "/v1/global/remote/status";
    public const string VEHICLE_GLOBAL_REMOTE_ELECTRIC_STATUS_ENDPOINT = "/v1/global/remote/electric/status";
    public const string VEHICLE_TELEMETRY_ENDPOINT                     = "/v3/telemetry";
    public const string VEHICLE_NOTIFICATION_HISTORY_ENDPOINT          = "/v2/notification/history";
    public const string VEHICLE_SERVICE_HISTORY_ENDPONT                = "/v1/servicehistory/vehicle/summary";
    // t.b.d.   public const string CUSTOMER_ACCOUNT_ENDPOINT                      = "TBD";
    // t.b.d.   public const string VEHICLE_ASSOCIATION_ENDPOINT                   = "/v1/vehicle-association/vehicle";
    // t.b.d.   public const string VEHICLE_TRIPS_ENDPOINT                         = "/v1/trips?from={from_date}&to={to_date}&route={route}&summary={summary}&limit={limit}&offset={offset}";
    #endregion



    #region ------------- Fields --------------------------------------------------------------
    private Action<string> _logger;
    private string         _username;
    private string         _password;
    private string         _refreshToken;
    private int            _timeoutInSeconds = 60;
    private bool           _bypassSslValidation = true;
    private string         _refresh_token;
    private RestClient     _client;
    private TokenCacheItem _tokenCache;
    private bool           _useTokenCaching = true;
    private string         _tokenCacheFilename = "toyota_credentials_cache_contains_secrets.json";
    #endregion



    #region ------------- Init ----------------------------------------------------------------
    public MyToyota()
    {
    }
    #endregion



    #region ------------- Methods -------------------------------------------------------------
    public MyToyota UseCredentials(string username, string password)
    {
        _username = username;
        _password = password;
        return this;
    }

    public MyToyota UseLogger(Action<string> logger)
    {
        _logger = logger;
        return this;
    }

    public MyToyota UseTimeout(int timeoutSeconds)
    {
        _timeoutInSeconds = timeoutSeconds;
        return this;
    }

    public MyToyota UseTokenCacheFilename(string tokenCacheFilename)
    {
        _tokenCacheFilename = tokenCacheFilename;
        return this;
    }

    public MyToyota UseTokenCaching(bool useTokenCaching)
    {
        _useTokenCaching = useTokenCaching;
        return this;
    }

    public void Login()
    {
        if (System.IO.File.Exists(_tokenCacheFilename))
        {
            var content = System.IO.File.ReadAllText(_tokenCacheFilename);
            _tokenCache = JsonConvert.DeserializeObject<TokenCacheItem>(content);
        }

        _client = CreateRestClient();

        if (!IsTokenValid())
            UpdateToken();
    }

    public VehiclesModel GetVehicles()
    {
        return Get<VehiclesModel>(VEHICLE_GUID_ENDPOINT);
    }

    public ElectricResponseModel GetElectric(string vin)
    {
        return Get<ElectricResponseModel>(VEHICLE_GLOBAL_REMOTE_ELECTRIC_STATUS_ENDPOINT, vin);
    }

    public LocationResponseModel GetLocation(string vin)
    {
        return Get<LocationResponseModel>(VEHICLE_LOCATION_ENDPOINT, vin);
    }

    public HealthStatusResponseModel GetHealthStatus(string vin)
    {
        return Get<HealthStatusResponseModel>(VEHICLE_HEALTH_STATUS_ENDPOINT, vin);
    }

    public TelemetryStatusResponseModel GetTelemetryStatus(string vin)
    {
        return Get<TelemetryStatusResponseModel>(VEHICLE_TELEMETRY_ENDPOINT, vin);
    }

    public NotificationsResponseModel GetNotifications(string vin)
    {
        return Get<NotificationsResponseModel>(VEHICLE_NOTIFICATION_HISTORY_ENDPOINT, vin);
    }

    public RemoteStatusResponseModel GetRemoteStatus(string vin)
    {
        return Get<RemoteStatusResponseModel>(VEHICLE_GLOBAL_REMOTE_STATUS_ENDPOINT, vin);
    }

    public ServiceHistoryResponseModel GetServiceHistory(string vin)
    {
        return Get<ServiceHistoryResponseModel>(VEHICLE_SERVICE_HISTORY_ENDPONT, vin);
    }
    #endregion



    #region ------------- Implementation ------------------------------------------------------
    private void UpdateToken()
    {
        // Login to toyota servers and retrieve token and uuid for the account.
        if (!IsTokenValid())
        {
            if (_refreshToken != null)
            {
                try
                {
                    RefreshTokens();
                    return;
                }
                catch (Exception ex)
                {
                    throw new Exception($"could not refresh the access token!", ex);
                }
            }
            Authenticate();
        }
    }

    private void Authenticate()
    {
        #region --------------- retrieve access token ---------------------------------------------
        _logger("Authenticating");
        AuthenticationModel data = null;
        AuthenticationModel2? tokenInfo = null;

        for (int i=0; i < 10; i++)
        {
            var request = new RestRequest(AUTHENTICATE_URL, Method.Post);
            request.Timeout = TimeSpan.FromSeconds(_timeoutInSeconds);

            if (data is not null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.AddJsonBody(json);
            }

            var response = _client.ExecutePost(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("could not authenticate with Toyota server");

            try
		    {
                if (response.Content.Contains("tokenId"))
                {
                    tokenInfo = JsonConvert.DeserializeObject<AuthenticationModel2>(response.Content);
                    break;
                }

                data = JsonConvert.DeserializeObject<AuthenticationModel>(response.Content);
                foreach(var cb in data.callbacks)
                {
                    if (cb.type == "NameCallback" && cb.output.First().value.ToString() == "User Name")
                        cb.input.First().value = _username;
                    else if (cb.type == "PasswordCallback")
                        cb.input.First().value = _password;
                    else if (cb.type == "TextOutputCallback" && cb.output.First().value.ToString() == "User Not Found")
                        throw new Exception("ToyotaInvalidUsernameError, Authentication Failed. User Not Found.");
                }
		    }
            catch (Exception ex)
		    {
                throw new Exception("could not deserialize Authentication result from Toyota authenticate URL");
		    }
        }

        if (tokenInfo is null)
            throw new Exception("could not authenticate with Toyota");
        #endregion        

        #region --------------- authorize ----------------------------------------------------------
        var request2 = new RestRequest(AUTHORIZE_URL, Method.Get);
        request2.Timeout = TimeSpan.FromSeconds(_timeoutInSeconds);
        request2.AddHeader("cookie", $"iPlanetDirectoryPro={tokenInfo.tokenId}");

        var response2 = _client.ExecuteGet(request2);
        if (response2.StatusCode != System.Net.HttpStatusCode.OK && 
            response2.StatusCode != System.Net.HttpStatusCode.Found)
            throw new Exception("Authorization failed. {response}");

        string authenticationCode = null;
        try
		{
            var responseHeaders = response2.Headers;
            var locationHeader = responseHeaders.Where(h => h.Name == "Location").FirstOrDefault();
            var locationValue = locationHeader.Value;
            var location = HttpUtility.ParseQueryString(locationValue);
            authenticationCode = location["com.toyota.oneapp:/oauth2Callback?code"];
		}
        catch (Exception ex)
		{
            throw new Exception("could not deserialize Authentication result from Toyota authenticate URL");
		}

        if (authenticationCode is null)
            throw new Exception("could not retrieve the authentication code from Authorize URL");
        #endregion        

        #region --------------- retrieve tokens -----------------------------------------------------
        AccessTokenEndpointResponse? accessTokenData = null;
        try
        {
            var request3 = new RestRequest(ACCESS_TOKEN_URL, Method.Post);
            request3.Timeout = TimeSpan.FromSeconds(_timeoutInSeconds);
            request3.AddHeader("authorization", "basic b25lYXBwOm9uZWFwcA==");

            var tokenRequestData = new Dictionary<string, string>
            {
                { "client_id"    , "oneapp" },
                { "code"         , authenticationCode },
                { "redirect_uri" , "com.toyota.oneapp:/oauth2Callback" },
                { "grant_type"   , "authorization_code" },
                { "code_verifier", "plain" }
            };
            var req = new FormUrlEncodedContent(tokenRequestData).ReadAsStringAsync().GetAwaiter().GetResult();
            request3.AddBody(req, ContentType.FormUrlEncoded);
        
            var response3 = _client.ExecutePost(request3);
            if (response3.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"could not authenticate with Toyota server. Response: {response3.Content}");
        
            if (response3.Content is null)
                throw new Exception("could not retrieve token from ACCESS_TOKEN_URL");
        
            accessTokenData = JsonConvert.DeserializeObject<AccessTokenEndpointResponse>(response3.Content!);
            if (accessTokenData is null)
                throw new Exception("could not retrieve access token Toyota access token URL");

            if (string.IsNullOrEmpty(accessTokenData.access_token ) || 
                string.IsNullOrEmpty(accessTokenData.id_token     ) || 
                string.IsNullOrEmpty(accessTokenData.refresh_token) || 
                accessTokenData.expires_in == 0)
                throw new Exception("not enough data from Toyota access token URL");
        }
        catch (Exception ex)
		{
            throw new Exception("could not retrieve access token Toyota access token URL");
		}

        UpdateTokens(accessTokenData!);

        #endregion        
    }

    private void UpdateTokens(AccessTokenEndpointResponse? data)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(data.id_token);
        var uuid = jwtToken.Claims.First(claim => claim.Type == "uuid").Value;

        var tokenExpirationTime = DateTime.Now.AddSeconds(Convert.ToDouble(data.expires_in));

        _tokenCache = new TokenCacheItem()
        {
            access_token  = data.access_token,
            refresh_token = data.refresh_token,
            uuid          = uuid,
            expiration    = tokenExpirationTime,
            username      = _username
        };
        
        System.IO.File.WriteAllText(_tokenCacheFilename, JsonConvert.SerializeObject(_tokenCache));
    }

    private async Task RefreshTokens()
    {
		//
       // //if (_usePasshashAuthentication || _useApitokenAuthentication)
       // //{
       // //    if (_authentication is null)
       // //        _authentication = await GetPassHash(client);
       // //    if (_authentication is not null)
       // //        requestUrl += _authentication;
       // //} 
       // 
       // var request = new RestRequest(ACCESS_TOKEN_URL, Method.Get);
       // request.Timeout = TimeSpan.FromSeconds(_timeoutInSeconds);
       // //request.AddHeader("authorization", "basic b25lYXBwOm9uZWFwcA==");
       //
       // var body = new AccessTokenDTO()
       // {
       //     client_id = "oneapp",
       //     redirect_uri = "com.toyota.oneapp:/oauth2Callback",
       //     grant_type = "refresh_token",
       //     code_verifier = "plain",
       //     refresh_token = _refresh_token
       // };
       // request.AddJsonBody(JsonConvert.SerializeObject(body));
       //
       // var response = client.ExecuteGet(request);
       // if (response.StatusCode != System.Net.HttpStatusCode.OK)
       //     throw new ToyotaLoginError("could not authenticate with Toyota server");
       //
       // //_update_tokens(response.json());
    }    

    private bool IsTokenValid()
    {
        // Implement token validation logic
        return _tokenCache != null && 
            !string.IsNullOrEmpty(_tokenCache.access_token) &&
            _tokenCache.expiration > DateTime.Now;
    }

    private T Get<T>(string endpoint, string vin = null)
    {
        var request = CreateGetRequest(endpoint, vin);
        var response = Execute(request);

        try
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        catch (Exception ex)
        {
            throw new Exception($"could not deserialize {nameof(T)} from Toyota API", ex);
        }
    }

    private RestClient CreateRestClient()
    {
        //bypass ssl validation check by using RestClient object
        var options = new RestClientOptions();
        if (_bypassSslValidation)
            options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

        var client = new RestClient(options);
        return client;
    }

    private RestRequest CreateGetRequest(string endpoint, string? vin = null)
    {
        var uuid4 = Guid.NewGuid().ToString("D").ToUpperInvariant(); // "D" - xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx (32 digits separated by hyphens)

        var request = new RestRequest(API_BASE_URL + endpoint, Method.Get);
        request.Timeout = TimeSpan.FromSeconds(_timeoutInSeconds);
        request.AddHeader("x-api-key"      , API_KEY);
        request.AddHeader("API_KEY"        , API_KEY);
        request.AddHeader("x-guid"         , _tokenCache.uuid);
        request.AddHeader("guid"           , _tokenCache.uuid);
        request.AddHeader("x-client-ref"   , generate_hmac_sha256(CLIENT_VERSION, _tokenCache.uuid));
        request.AddHeader("x-correlationid", uuid4);
        request.AddHeader("x-appversion"   , CLIENT_VERSION);
        request.AddHeader("x-region"       , "EU");
        request.AddHeader("authorization"  , $"Bearer {_tokenCache.access_token}");
        request.AddHeader("x-channel"      , "ONEAPP");
        request.AddHeader("x-brand"        , "T");
        request.AddHeader("user-agent"     , "okhttp/4.10.0");


        if (vin is not null)
            request.AddHeader("vin", vin);

        return request;
    }

    private RestResponse Execute(RestRequest request)
    {
        var response = _client.ExecuteGet(request);
        if (response.StatusCode != System.Net.HttpStatusCode.OK &&
            response.StatusCode != System.Net.HttpStatusCode.Found)
            throw new Exception($"Could not retrieve vehicles from Totoya API {response.StatusCode} {response.StatusDescription}");
        return response;
    }

    private string generate_hmac_sha256(string key, string message)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(key)))
        {
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
    #endregion
}
