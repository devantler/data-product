using Devantler.DataMesh.DataProduct.Extensions;
using Devantler.DataMesh.DataProduct.Features;

#pragma warning disable CA1852

var webApplicationOptions = new WebApplicationOptions
{
    WebRootPath = "Features/Dashboard/UI/wwwroot"
};
var builder = WebApplication.CreateBuilder(webApplicationOptions);
_ = builder.Configuration.AddDataProductConfiguration(builder.Environment, args);
builder.AddFeatures();

var app = builder.Build();
app.UseFeatures();
app.Run();

#pragma warning restore CA1852
