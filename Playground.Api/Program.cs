using System.Diagnostics;
using Playground.Api.OpenApi;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.EnableAnnotations(); });

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
        [SwaggerOperationFilter(typeof(FileUploadOperationFilter))]
        (WeatherForecastUpload weatherForecastUpload) =>
        {
            Debug.WriteLine(weatherForecastUpload.SignatureHash);
            Debug.WriteLine(weatherForecastUpload.SignatureHorodate);
            Debug.WriteLine(weatherForecastUpload.File.Name);
            Debug.WriteLine(weatherForecastUpload.File.FileName);
            Debug.WriteLine(weatherForecastUpload.File.ContentType);

            return Results.Ok("citron");
        })
    .Produces<string>(200)
    .WithName("UploadFile")
    .Accepts<WeatherForecastUpload>("multipart/form-data")
    .WithOpenApi();


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}