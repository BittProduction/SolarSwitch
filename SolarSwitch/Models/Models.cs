using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SolarSwitch.Models;

public class LoginRequestModel
{
    /// <summary>
    /// Unique user reference
    /// </summary>
    [JsonProperty("username")]
    public string User { get; set; }

    /// <summary>
    /// Password used by user
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; }
}

public class LoginResultModel
{
    [JsonProperty("refreshToken")]
    public string refreshToken { get; set; }
    
    [JsonProperty("token")]
    public string Token { get; set; }
}

public class SenecSystemRequestModel
{
    [JsonProperty("SENEC_TOKEN")]
    public string Token { get; set; }
}

public class SenecSystemResultModel
{
    [JsonProperty("id")]
    public string ID { get; set; }
    [JsonProperty("steuereinheitnummer")]
    public string Steuereinheitnummer { get; set; }
    [JsonProperty("gehaeusenummer")]
    public string Gehaeusenummer { get; set; }
    [JsonProperty("strasse")]
    public string Strasse { get; set; }
    [JsonProperty("hausnummer")]
    public string Hausnummer { get; set; }
    [JsonProperty("postleitzahl")]
    public string Postleitzahl { get; set; }
    [JsonProperty("ort")]
    public string Ort { get; set; }
    [JsonProperty("laendercode")]
    public string Laendercode { get; set; }
    [JsonProperty("zeitzone")]
    public string Zeitzone { get; set; }
    [JsonProperty("wallboxIds")]
    public List<string> WallboxIds { get; set; }
    [JsonProperty("systemType")]
    public string SystemType { get; set; }
}

public class SenecDashboardRequestModel
{
    [JsonProperty("SENEC_ANLAGE")]
    public string Anlage { get; set; }
    
    [JsonProperty("SENEC_TOKEN")]
    public string Token { get; set; }
}

public class SenecDashboardResultModel
{
    [JsonProperty("currently")]
    public SenecDashboardResultModel_Currently Currently { get; set; }
    
    [JsonProperty("today")]
    public SenecDashboardResultModel_Today Today { get; set; }
    
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
    
    [JsonProperty("electricVehicleConnected")]
    public string ElectricVehicleConnected { get; set; }
}

public class SenecDashboardResultModel_Currently
{
    [JsonProperty("powerGenerationInW")]
    public double PowerGenerationInW { get; set; }
    
    [JsonProperty("powerConsumptionInW")]
    public double PowerConsumptionInW { get; set; }
    
    [JsonProperty("gridFeedInInW")]
    public double GridFeedInInW { get; set; }
    
    [JsonProperty("gridDrawInW")]
    public double GridDrawInW { get; set; }
    
    [JsonProperty("batteryChargeInW")]
    public double BatteryChargeInW { get; set; }
    
    [JsonProperty("batteryDischargeInW")]
    public double BatteryDischargeInW { get; set; }
    
    [JsonProperty("batteryLevelInPercent")]
    public double BatteryLevelInPercent { get; set; }
    
    [JsonProperty("selfSufficiencyInPercent")]
    public double SelfSufficiencyInPercent { get; set; }
    
    [JsonProperty("wallboxInW")]
    public double WallboxInW { get; set; }
}

public class SenecDashboardResultModel_Today
{
    [JsonProperty("powerGenerationInWh")]
    public double PowerGenerationInWh { get; set; }
    
    [JsonProperty("powerConsumptionInWh")]
    public double PowerConsumptionInWh { get; set; }
    
    [JsonProperty("gridFeedInInWh")]
    public double GridFeedInInWh { get; set; }
    
    [JsonProperty("gridDrawInWh")]
    public double GridDrawInWh { get; set; }
    
    [JsonProperty("batteryChargeInWh")]
    public double BatteryChargeInWh { get; set; }
    
    [JsonProperty("batteryDischargeInWh")]
    public double BatteryDischargeInWh { get; set; }
    
    [JsonProperty("batteryLevelInPercent")]
    public double BatteryLevelInPercent { get; set; }
    
    [JsonProperty("selfSufficiencyInPercent")]
    public double SelfSufficiencyInPercent { get; set; }
    
    [JsonProperty("wallboxInWh")]
    public double WallboxInWh { get; set; }
}

