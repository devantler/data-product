// using System;
// using System.IO;
// using System.Reflection;
// using Devantler.DataMesh.Core.Extensions;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.SwaggerGen;

// namespace Devantler.DataMesh.Core.Extensions;

// public static class SwaggerExtensions
// {
//     public static IServiceCollection AddSwaggerGen(this IServiceCollection services, string title, string version, string description, OpenApiContact contact, OpenApiLicense license)
//     {
//         _ = services.AddSwaggerGen(options =>
//         {
//             {
//                 options.SwaggerDoc(version, new OpenApiInfo
//                 {
//                     Version = version,
//                     Title = title,
//                     Description = description,
//                     Contact = contact,
//                     License = license
//                 });
//                 options.IncludeXmlComments();
//             }
//         });
//         return services;
//     }

//     public static void IncludeXmlComments(this SwaggerGenOptions options)
//     {
//         string xmlFileName = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
//         string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
//         options.IncludeXmlComments(xmlPath);
//     }
// }
