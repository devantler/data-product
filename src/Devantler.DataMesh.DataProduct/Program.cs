using Devantler.DataMesh.DataProduct.Extensions;
using Devantler.DataMesh.DataProduct.Features;

#pragma warning disable CA1852

var builder = WebApplication.CreateBuilder();
_ = builder.Configuration.AddDataProductConfiguration(builder.Environment, args);
builder.WebHost.UseStaticWebAssets();
builder.AddFeatures();

var app = builder.Build();
app.UseFeatures();
app.Run();

#pragma warning restore CA1852
