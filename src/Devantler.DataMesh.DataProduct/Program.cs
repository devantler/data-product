#pragma warning disable CA1852
using Devantler.DataMesh.DataProduct;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddFeatures(builder.Configuration);

WebApplication app = builder.Build();
app.UseHttpsRedirection();

app.UseFeatures(builder.Configuration);
app.Run();
