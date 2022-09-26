using System.Reflection;
using DevAntler.DataMesh.DataProductService.Extensions;
using DevAntler.DataMesh.SwaggerGenExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSwaggerDoc("Data Product API", "v1", "A Web API for managing Data Products");
    options.IncludeXmlComments();
});
builder.Services.AddProvisioningTargets();

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