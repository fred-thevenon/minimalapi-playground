using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Playground.Api.OpenApi;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.RequestBody = null;

        var file = nameof(WeatherForecastUpload.File).ToLower();
        var signatureHash = nameof(WeatherForecastUpload.SignatureHash).ToLower();
        var signatureHorodate = nameof(WeatherForecastUpload.SignatureHorodate).ToLower();
        var uploadFileMediaType = new OpenApiMediaType()
        {
            Schema = new OpenApiSchema
            {
                Properties = new Dictionary<string, OpenApiSchema>()
                {
                    {
                        file,
                        new OpenApiSchema { Type = "string", Format = "binary", Nullable = false }
                    },
                    {
                        signatureHash,
                        new OpenApiSchema { Type = "string", Format = "string", Nullable = false }
                    },
                    {
                        signatureHorodate,
                        new OpenApiSchema { Type = "string", Format = "string", Description = "RFC 3339 datetime format", Nullable = false }
                    }
                },
                Required = new SortedSet<string> { file, signatureHorodate, signatureHash }
            },
            Encoding = new Dictionary<string, OpenApiEncoding>()
            {
                { file, new OpenApiEncoding() { Style = ParameterStyle.Form } },
                { signatureHorodate, new OpenApiEncoding() { Style = ParameterStyle.Form } },
                { signatureHash, new OpenApiEncoding() { Style = ParameterStyle.Form } },
            }
        };
        operation.RequestBody = new OpenApiRequestBody
        {
            Content = { ["multipart/form-data"] = uploadFileMediaType }
        };
    }
}