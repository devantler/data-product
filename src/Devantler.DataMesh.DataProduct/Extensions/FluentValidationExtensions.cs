// using FluentValidation.Results;
// using Microsoft.AspNetCore.Mvc.ModelBinding;

// namespace Devantler.DataMesh.DataProduct.Web.Extensions;

// public static class FluentValidationExtensions
// {
//     //TODO: Rework this to use the approach Rasmus implemented on Umbraco.Heartcore.Cms
//     public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
//     {
//         foreach (ValidationFailure error in result.Errors)
//         {
//             modelState.AddModelError(error.PropertyName, error.ErrorMessage);
//         }
//     }
// }

