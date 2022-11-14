using Devantler.DataMesh.Core.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    "Data Mesh Provisioning",
    "v1.0.0",
    "An API to provision and manage data mesh infrastructure and data products.",
    new OpenApiContact
    {
        Name = "Devantler",
        Email = "nikolaiemildamm@icloud.com"
    },
    new OpenApiLicense
    {
        Name = "MIT",
        Url = new Uri("https://opensource.org/licenses/MIT")
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
