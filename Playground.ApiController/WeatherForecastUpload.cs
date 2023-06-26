namespace Playground.ApiController;

public class WeatherForecastUpload
{
    public IFormFile File { get; set; }
    public DateTimeOffset SignatureDate { get; set; }
    public string SignatureHash { get; set; }
}