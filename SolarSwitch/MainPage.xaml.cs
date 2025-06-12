using Newtonsoft.Json;
using SolarSwitch.Models;
using SolarSwitch.Service;

namespace SolarSwitch;

public partial class MainPage : ContentPage
{
    
    HttpClient _httpClient = new HttpClient();

    private string _loginURL { get; } = "https://app-gateway.prod.senec.dev/v1/senec/login";
    
    
    public MainPage()
    {
        InitializeComponent();
        
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        LoginRequestModel loginModel = new LoginRequestModel
        {
            User = "Familiebittnerwaldkirch@gmail.com",
            Password = "M.Counter#Senec2807"
        };

        try
        {
            var result = await HTTPService.Instance.LoginAsync(loginModel);
            // convert the result to JSON
            var jsonResponse = await result.Content.ReadAsStringAsync();
            // Deserialize the JSON response to SenecAuthenticationModel
            var senecAuth = JsonConvert.DeserializeObject<LoginResultModel>(jsonResponse);
            
            await HTTPService.Instance.GetSenecDashboardAsync(new SenecDashboardRequestModel()
            {
                Anlage = "317348",
                Token = senecAuth.Token
            });
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
        
    }
}