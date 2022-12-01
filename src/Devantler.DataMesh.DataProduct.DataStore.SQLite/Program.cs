using Devantler.DataMesh.DataProduct.DataStore.SQLite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSQLiteService();

var app = builder.Build();
app.UseSQLiteService();
app.Run();
