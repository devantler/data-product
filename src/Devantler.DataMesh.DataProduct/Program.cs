using Devantler.DataMesh.DataProduct;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFeatureManagement();
builder.Services.AddDomain(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();

app.UseDomain();
app.Run();
