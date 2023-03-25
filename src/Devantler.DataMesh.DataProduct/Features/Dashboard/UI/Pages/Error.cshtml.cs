﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Devantler.DataMesh.DataProduct.Features.Dashboard.UI.Pages;

/// <summary>
/// The error page model.
/// </summary>
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    /// <summary>
    /// The ID of the request that caused the error.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether or not the request ID should be shown.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

#pragma warning disable IDE0052 // Remove unread private members
    readonly ILogger<ErrorModel> _logger;
#pragma warning restore IDE0052 // Remove unread private members
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorModel"/> class.
    /// </summary>
    public ErrorModel(ILogger<ErrorModel> logger)
        => _logger = logger;

    /// <summary>
    /// Sets the request ID when the error page is loaded.
    /// </summary>
    public void OnGet()
        => RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
}
