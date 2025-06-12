using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolarSwitch.Models;

namespace SolarSwitch.Service;

public class HTTPService
{
    private HttpClient _httpClient = new HttpClient();
    
    private string _loginURL { get; } = "https://app-gateway.prod.senec.dev/v1/senec/login";
    private string _systemURL { get; } = "https://app-gateway.prod.senec.dev/v1/senec/systems";
    private string _dashboardURL { get; } = "https://app-gateway.prod.senec.dev/v2/senec/systems/{{SENEC_ANLAGE}}/dashboard";

    private const string SENEC_ID = "317348";
    
    // Generate a lazy-loaded implementation of this HTTPService
    private static Lazy<HTTPService> _instance = new Lazy<HTTPService>(() => new HTTPService());
    public static HTTPService Instance => _instance.Value;
    
    private HTTPService()
    {
        // Initialize the HttpClient or any other resources here if needed
        _httpClient.Timeout = TimeSpan.FromSeconds(30); // Set a timeout for requests
    }
    
    public async Task<HttpResponseMessage> LoginAsync(LoginRequestModel loginModel)
    {
        var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_loginURL, content);
        
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Deserialize the JSON response to SenecAuthenticationModel
            var senecAuth = JsonConvert.DeserializeObject<LoginResultModel>(jsonResponse);
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", senecAuth.Token);
            return response;
        }
        
        throw new Exception($"Login failed with status code: {response.StatusCode}");
    }
    
    public async Task<SenecSystemResultModel> GetSenecSystemAsync(SenecSystemRequestModel _SystemRequestModel)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _systemURL);
        request.Headers.Add("authentication", _SystemRequestModel.Token);
        request.Headers.Add("Host", "app-gateway.prod.senec.dev");
        request.Headers.Add("Cache-Control", "no-cache");
        request.Headers.Add("Accept","*/*");
        request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
        request.Headers.Add("Connection", "keep-alive");
        
        var response = await _httpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SenecSystemResultModel>(jsonResponse);
        }
        
        throw new Exception($"Failed to retrieve Senec system with status code: {response.StatusCode}");
    }

    public async Task<SenecDashboardResultModel> GetSenecDashboardAsync(SenecDashboardRequestModel _SenecDashboardRequestModel)
    {
        
        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "https://app-gateway.prod.senec.dev/v1/senec/login");
        loginRequest.Content = new StringContent(JsonConvert.SerializeObject(new
        {
            username = "Familiebittnerwaldkirch@gmail.com",
            password = "M.Counter#Senec2807"
        }), Encoding.UTF8, "application/json");

        var loginResponse = await _httpClient.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        dynamic loginResult = JsonConvert.DeserializeObject(loginResponseBody);
        string token = loginResult?.token?.ToString(); 
        
        
        var dashboardRequest = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://app-gateway.prod.senec.dev/v2/senec/systems/{_SenecDashboardRequestModel.Anlage}/dashboard");

        dashboardRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dashboardResponse = await _httpClient.SendAsync(dashboardRequest);
        dashboardResponse.EnsureSuccessStatusCode();

        string dashboardBody = await dashboardResponse.Content.ReadAsStringAsync();
        var senecDashboard = JsonConvert.DeserializeObject<SenecDashboardResultModel>(dashboardBody);

        if (senecDashboard == null)
        {
            throw new Exception("Failed to deserialize Senec dashboard data.");
        }
        return senecDashboard;
    }
}