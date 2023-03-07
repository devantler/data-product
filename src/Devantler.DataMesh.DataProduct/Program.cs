#pragma warning disable IDE0210

using Devantler.DataMesh.DataProduct.Features;

namespace Devantler.DataMesh.DataProduct;

sealed class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string fileExtension = File.Exists("data-product-config.yaml") ? "yaml" : "yml";

        _ = builder.Configuration
            .AddJsonFile("appsettings.json", optional: false)
            .AddYamlFile($"data-product-config.{fileExtension}", optional: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        builder.AddFeatures();

        var app = builder.Build();
        app.UseFeatures();
        app.Run();
    }
}
#pragma warning restore IDE0210
