using Devantler.DataMesh.DataProduct;
using Devantler.DataMesh.DataProduct.Configuration.Extensions;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
_ = builder.Services.AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key));
var dataProductOptions = builder.Configuration.GetDataProductOptions();
builder.Services.AddFeatures(dataProductOptions, builder.Environment);

var app = builder.Build();
app.UseFeatures(dataProductOptions);
app.Run();
