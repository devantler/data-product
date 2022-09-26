using Devantler.DataMesh.Services.Core.Extensions;
using Devantler.DataMesh.Services.Dataspace.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSwaggerDoc("Dataspace API", "v1", "A Web API for managing Dataspaces");
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