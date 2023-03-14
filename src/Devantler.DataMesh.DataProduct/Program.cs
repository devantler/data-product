using Devantler.DataMesh.DataProduct;
using Devantler.DataMesh.DataProduct.Features;

#pragma warning disable CA1852

var builder = WebApplication.CreateBuilder(args);

_ = builder.Configuration.AddDataProductConfiguration(builder.Environment, args);
builder.AddFeatures();

var app = builder.Build();
app.UseFeatures();
app.Run();

#pragma warning restore CA1852
