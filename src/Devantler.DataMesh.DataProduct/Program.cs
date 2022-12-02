using Devantler.DataMesh.DataProduct.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFeatures(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();

app.UseFeatures(builder.Configuration);
app.Run();
