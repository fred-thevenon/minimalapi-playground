namespace WeatherService;

public partial class WeatherClient
{
    /// <param name="signatureHorodate">RFC 3339 datetime format</param>
    /// <returns>OK</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual System.Threading.Tasks.Task<string> UploadFileAsync(FileParameter file, string signatureHash, DateTimeOffset signatureHorodate)
    {
        return UploadFileAsync(file, signatureHash, signatureHorodate.ToString("o"), System.Threading.CancellationToken.None);
    }
}