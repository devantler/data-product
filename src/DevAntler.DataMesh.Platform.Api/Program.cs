using DevAntler.DataMesh.Platform.Provisioning;
using DevAntler.DataMesh.Platform.Provisioning.Local;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if(builder.Environment.IsDevelopment())
    builder.Services.AddTransient<IDataProductProvisioningService, LocalDataProductProvisioningService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
