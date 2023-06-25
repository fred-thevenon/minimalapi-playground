using System.Diagnostics;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<WeatherForecastUpload>(() => new OpenApiSchema()
    {
        Type = "object",
        Properties = new Dictionary<string, OpenApiSchema>()
        {
            {
                "file",
                new OpenApiSchema { Type = "string", Format = "binary", Required = new HashSet<string> { "true" }, Nullable = false}
            },
            {
                "date",
                new OpenApiSchema { Type = "string", Format = "date-time", Required = new HashSet<string> { "true" },Nullable = false }
            }
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapPost("/weatherforecast/upload",
        (WeatherForecastUpload weatherForecastUpload) =>
        {
            Debug.WriteLine(weatherForecastUpload.Date);
            Debug.WriteLine(weatherForecastUpload.File.Name);
            Debug.WriteLine(weatherForecastUpload.File.FileName);
            Debug.WriteLine(weatherForecastUpload.File.ContentType);

            return Results.Ok("citron");
        })
    .Produces<string>(200)
    .Accepts<WeatherForecastUpload>("multipart/form-data");

app.Run();

internal class WeatherForecastUpload
{
    public IFormFile File { get; set; }
    public DateTime Date { get; set; }

    public static async ValueTask<WeatherForecastUpload?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        var form = await httpContext.Request.ReadFormAsync();
        var file = form.Files["file"]!;
        var date = DateTime.Now;

        return
            new WeatherForecastUpload()
            {
                File = file,
                Date = date
            };
    }
}

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
