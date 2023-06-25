// See https://aka.ms/new-console-template for more information

using WeatherService;

Console.WriteLine("Hello, World!");

using var httpClient = new HttpClient();
var weatherClient = new WeatherClient("https://localhost:7075/", httpClient);
var result = await weatherClient.UploadAsync(new FileParameter(File.OpenRead(@"C:\Users\FrédéricTHEVENON\Downloads\adhesion_649041e3c67cd16b1c0377fc.pdf")), DateTimeOffset.Now.DateTime);
Console.WriteLine(result);
