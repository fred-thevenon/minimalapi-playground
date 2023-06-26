using System.Globalization;
using System.Reflection;
using Playground.Api;
using Swashbuckle.AspNetCore.Annotations;

internal class WeatherForecastUpload
{
    public IFormFile File { get; set; }
    public DateTimeOffset SignatureHorodate { get; set; }
    public string SignatureHash { get; set; }

    public static async ValueTask<WeatherForecastUpload?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        var form = await httpContext.Request.ReadFormAsync();
        var file = form.Files[nameof(File).ToLower()]!;
        var signatureHash =  form[nameof(SignatureHash).ToLower()];
        var signatureHorodate = DateTimeOffset.Parse(form[nameof(SignatureHorodate).ToLower()],null, DateTimeStyles.RoundtripKind);

        return
            new WeatherForecastUpload
            {
                File = file,
                SignatureHorodate = signatureHorodate,
                SignatureHash = signatureHash!
            };
    }
}