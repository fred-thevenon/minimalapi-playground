namespace Playground.ApiController;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public class WeatherForecastUpload
{
    public IFormFile File { get; set; }
    public DateTimeOffset SignatureDate { get; set; }
    public string SignatureHash { get; set; }
}