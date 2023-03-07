#pragma warning disable IDE0210

using Devantler.DataMesh.DataProduct.Features;

namespace Devantler.DataMesh.DataProduct;

sealed class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string dataProductConfigFile = File.Exists("data-product-config.yaml") ? "data-product-config.yaml" : "data-product-config.yml";

        _ = builder.Configuration
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddYamlFile(dataProductConfigFile, optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        builder.AddFeatures();

        var app = builder.Build();
        app.UseFeatures();
        app.Run();
    }
}
#pragma warning restore IDE0210
