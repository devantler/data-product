#pragma warning disable CA1852
using Devantler.DataMesh.DataProduct;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFeatures(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();

app.UseFeatures(builder.Configuration);
app.Run();
