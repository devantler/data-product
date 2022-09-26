using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json")
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerForOcelotUI(opt => opt.PathToSwaggerGenerator = "/swagger/docs")
    .UseOcelot().Wait();

app.Run();