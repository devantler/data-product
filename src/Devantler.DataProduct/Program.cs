using Devantler.DataProduct.Core;
using Devantler.DataProduct.Features;

#pragma warning disable CA1852

var builder = WebApplication.CreateBuilder();
var options = builder.AddCore(args);
builder.AddFeatures(options);

var app = builder.Build();
app.UseFeatures();
app.Run();

#pragma warning restore CA1852