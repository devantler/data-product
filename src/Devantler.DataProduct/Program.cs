using Devantler.DataProduct.Features;

#pragma warning disable CA1852

var builder = WebApplication.CreateBuilder();
builder.AddFeatures(args);

var app = builder.Build();
app.UseFeatures();
app.Run();

#pragma warning restore CA1852