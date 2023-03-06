#pragma warning disable IDE0210

using Devantler.DataMesh.DataProduct.Features;


namespace Devantler.DataMesh.DataProduct;

sealed class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddFeatures();

        var app = builder.Build();
        app.UseFeatures();
        app.Run();
    }
}
#pragma warning restore IDE0210
